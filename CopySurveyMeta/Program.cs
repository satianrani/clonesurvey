using CopySurveyMeta.DataAccess;
using CopySurveyMeta.Model;
using CopySurveyMeta.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace CopySurveyMeta
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string publishedName = "q3xh0";
            //    DeleteData(publishedName, ApplicationInit.GetAppConfig.ConnectionStringDestination, true);
            InsertData(publishedName);
        }

        private static void DeleteData(string PublishedKey, string sqlConfig, bool hasDeleteSurvey = false)
        {
            DbConnect db = null;
            IDbTransaction dbTransaction = null;

            try
            {
                db = new DbConnect(sqlConfig);
                var dbConnection = db.GetConnection();
                dbTransaction = dbConnection.BeginTransaction();

                var dbSurveyPublished = new RepositoryBase<SurveyPublished>(dbConnection, dbTransaction).Query(new { PublishedKey = PublishedKey }, $"where PublishedKey = @PublishedKey");
                if (!dbSurveyPublished.Any())
                {
                    throw new Exception("Survey Published not found");
                }
                var surveyMetaIDs = dbSurveyPublished.Select(x => x.SurveyMetaID);
                var dbSurveyMeta = new RepositoryBase<SurveyMeta>(dbConnection, dbTransaction).Query(new { ID = surveyMetaIDs }, $"where ID in @ID");
                //delete meta lang
                new RepositoryBase<SurveyMetaLanguage>(dbConnection, dbTransaction).Delete(new { SurveyMetaID = surveyMetaIDs }, $"where SurveyMetaID in @SurveyMetaID");
                // delete meta variable
                new RepositoryBase<SurveyMetaURLVariable>(dbConnection, dbTransaction).Delete(new { SurveyMetaID = surveyMetaIDs }, $"where SurveyMetaID in @SurveyMetaID");
                //delete SurveyMetaContent
                new RepositoryBase<SurveyMetaContent>(dbConnection, dbTransaction).Delete(new { SurveyMetaID = surveyMetaIDs }, $"where SurveyMetaID in @SurveyMetaID");
                //delete SurveyMetaElementContent
                new RepositoryBase<SurveyMetaElementContent>(dbConnection, dbTransaction).Delete(new { SurveyMetaID = surveyMetaIDs }, $"where SurveyMetaID in @SurveyMetaID");
                //delete SurveyMetaElementSubContent
                new RepositoryBase<SurveyMetaElementSubContent>(dbConnection, dbTransaction).Delete(new { SurveyMetaID = surveyMetaIDs }, $"where SurveyMetaID in @SurveyMetaID");
                new RepositoryBase<SurveyMetaElement>(dbConnection, dbTransaction).Delete(new { SurveyMetaID = surveyMetaIDs }, $"where SurveyMetaID in @SurveyMetaID");
                //delete SurveyPublished
                new RepositoryBase<SurveyPublished>(dbConnection, dbTransaction).Delete(new { PublishedKey = PublishedKey }, $"where PublishedKey = @PublishedKey");
                new RepositoryBase<SurveyMeta>(dbConnection, dbTransaction).Delete(new { ID = surveyMetaIDs }, $"where ID in @ID");
                if (hasDeleteSurvey)
                {
                    //delete survey
                    new RepositoryBase<Survey>(dbConnection, dbTransaction).Delete(new Survey { ID = dbSurveyPublished.FirstOrDefault().SurveyID }, $"where ID = @ID");
                }
                // insert commits
                dbTransaction.Commit();
            }
            catch (Exception e)
            {
                dbTransaction.Rollback();
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                }
            }
        }

        private static void InsertData(string PublishedKey)
        {
            DbConnect source = new DbConnect(ApplicationInit.GetAppConfig.ConnectionStringSource);
            DbConnect destination = null;
            IDbTransaction destinationTransaction = null;

            try
            {
                SurveyPublished sourceSurveyPublished = new RepositoryBase<SurveyPublished>(source).QueryFirst(new SurveyPublished { PublishedKey = PublishedKey }, $"where PublishedKey = @PublishedKey");
                if (sourceSurveyPublished == null)
                {
                    throw new Exception("Source SurveyPublished Table not found");
                }
                Survey sourceSurvey = new RepositoryBase<Survey>(source).QueryFirst(new Survey { ID = sourceSurveyPublished.SurveyID }, $"where ID = @ID");
                if (sourceSurvey == null)
                {
                    throw new Exception("Source Survey Table  not found");
                }
                var sourceSurveyMeta = new RepositoryBase<SurveyMeta>(source).QueryFirst(new SurveyMeta { SurveyID = sourceSurveyPublished.SurveyID }, $"where ID = @SurveyID");
                if (sourceSurveyMeta == null)
                {
                    throw new Exception("Source SurveyMeta Table  not found");
                }

                SurveyMetaContent sourceSurveyMetaContent = new RepositoryBase<SurveyMetaContent>(source).QueryFirst(new SurveyMetaContent { SurveyMetaID = sourceSurveyPublished.SurveyMetaID }, $"where SurveyMetaID = @SurveyMetaID");
                var sourceSurveyMetaElement = new RepositoryBase<SurveyMetaElement>(source).Query($"where SurveyMetaID = {sourceSurveyPublished.SurveyMetaID} order by [Index]");

                var sourceSurveyMetaLanguage = new RepositoryBase<SurveyMetaLanguage>(source).Query($"where SurveyMetaID = {sourceSurveyPublished.SurveyMetaID}");
                var sourceSurveyMetaURLVariable = new RepositoryBase<SurveyMetaURLVariable>(source).Query($"where SurveyMetaID = {sourceSurveyPublished.SurveyMetaID}");

                var sourceSurveyMetaElementContent = new RepositoryBase<SurveyMetaElementContent>(source).Query($"where SurveyMetaID  = {sourceSurveyPublished.SurveyMetaID}");
                var sourceSurveyMetaElementSubContent = new RepositoryBase<SurveyMetaElementSubContent>(source).Query($"where SurveyMetaID = {sourceSurveyPublished.SurveyMetaID}");

                destination = new DbConnect(ApplicationInit.GetAppConfig.ConnectionStringDestination);
                var destinationConnection = destination.GetConnection();
                destinationTransaction = destinationConnection.BeginTransaction();

                var destinationSurvey = new RepositoryBase<Survey>(destinationConnection, destinationTransaction).Insert(sourceSurvey);
                sourceSurveyMeta.SurveyID = destinationSurvey.ID;
                var destinationSurveyMeta = new RepositoryBase<SurveyMeta>(destinationConnection, destinationTransaction).Insert(sourceSurveyMeta);
                sourceSurveyPublished.SurveyID = destinationSurvey.ID;
                sourceSurveyPublished.SurveyMetaID = destinationSurveyMeta.ID;
                var destinationSurveyPublished = new RepositoryBase<SurveyPublished>(destinationConnection, destinationTransaction).Insert(sourceSurveyPublished);

                sourceSurveyMetaContent.SurveyMetaID = destinationSurveyMeta.ID;
                var destinationSurveyMetaContent = new RepositoryBase<SurveyMetaContent>(destinationConnection, destinationTransaction).Insert(sourceSurveyMetaContent);
                sourceSurveyMetaElement.ToList().ForEach(element =>
                {
                    var logSurveyMetaElementContent = sourceSurveyMetaElementContent.Where(v => v.SurveyMetaElementID == element.ID && v.SurveyMetaID == element.SurveyMetaID).ToList();
                    var logSurveyMetaElementSubContent = sourceSurveyMetaElementSubContent.Where(v => v.SurveyMetaElementID == element.ID && v.SurveyMetaID == element.SurveyMetaID).ToList();
                    element.SurveyMetaID = destinationSurveyMeta.ID;
                    element = new RepositoryBase<SurveyMetaElement>(destinationConnection, destinationTransaction).Insert(element);
                    logSurveyMetaElementContent.ForEach(content =>
                    {
                        content.SurveyMetaID = element.SurveyMetaID;
                        content.SurveyMetaElementID = element.ID;
                        new RepositoryBase<SurveyMetaElementContent>(destinationConnection, destinationTransaction).Insert(content);
                    });
                    logSurveyMetaElementSubContent.ForEach(sub =>
                    {
                        sub.SurveyMetaID = element.SurveyMetaID;
                        sub.SurveyMetaElementID = element.ID;
                        new RepositoryBase<SurveyMetaElementSubContent>(destinationConnection, destinationTransaction).Insert(sub);
                    });
                });
                sourceSurveyMetaLanguage.ToList().ForEach(lang =>
                {
                    lang.SurveyMetaID = destinationSurveyMeta.ID;
                    new RepositoryBase<SurveyMetaLanguage>(destinationConnection, destinationTransaction).Insert(lang);
                });
                sourceSurveyMetaURLVariable.ToList().ForEach(variables =>
                {
                    variables.SurveyMetaID = destinationSurveyMeta.ID;
                    new RepositoryBase<SurveyMetaURLVariable>(destinationConnection, destinationTransaction).Insert(variables);
                });

                // insert commits
                destinationTransaction.Commit();
            }
            catch (Exception e)
            {
                destinationTransaction.Rollback();
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (destination != null)
                {
                    destination.Dispose();
                }
                if (source != null)
                {
                    source.Dispose();
                }
            }
        }
    }
}