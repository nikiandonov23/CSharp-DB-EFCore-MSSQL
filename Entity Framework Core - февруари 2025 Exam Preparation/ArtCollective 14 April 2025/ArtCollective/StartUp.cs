using ArtCollective.Data;
using Microsoft.EntityFrameworkCore;

namespace ArtCollective
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            ArtCollectiveDbContext dbContext = new ArtCollectiveDbContext();

            ResetDatabase(dbContext, shouldDropDatabase: true);

            var projectDir = GetProjectDirectory();

            ImportEntities(dbContext, projectDir + @"Datasets/", projectDir + @"ImportResults/");
            ExportEntities(dbContext, projectDir + @"ExportResults/");

            using (var transaction = dbContext.Database.BeginTransaction())
            {
                transaction.Rollback();
            }
        }

        private static void ImportEntities(ArtCollectiveDbContext dbContext, string baseDir, string exportDir)
        {
            var feedbacks = DataProcessor.Deserializer
                .ImportFeedbacks(dbContext, File.ReadAllText(baseDir + "feedbacks.xml"));

            PrintAndExportEntityToFile(feedbacks, exportDir + "ActualResult_ImportFeedbacks.txt");

            var artworks = DataProcessor.Deserializer
                .ImportArtworks(dbContext, File.ReadAllText(baseDir + "artworks.json"));

            PrintAndExportEntityToFile(artworks, exportDir + "ActualResult_Artworks.txt");
        }

        private static void ExportEntities(ArtCollectiveDbContext dbContext, string exportDir)
        {
            var ArtistsWithTheirArtworks = DataProcessor.Serializer
                .ExportArtistsWithCollaborationsCountAndTheirArtworks(dbContext);

            Console.WriteLine(ArtistsWithTheirArtworks);
            File.WriteAllText(exportDir + "ActualResult_ExportedArtistsWithTheirArtworks.xml", ArtistsWithTheirArtworks);

            var Groups = DataProcessor.Serializer
                .ExportGroupsWithFeedbacksChronologically(dbContext);

            Console.WriteLine(Groups);
            File.WriteAllText(exportDir + "ActualResult_ExportedGroupsWithFeedbacks.json", Groups);
        }

        private static void PrintAndExportEntityToFile(string entityOutput, string outputPath)
        {
            Console.WriteLine(entityOutput);
            File.WriteAllText(outputPath, entityOutput.TrimEnd());
        }

        private static object GetProjectDirectory()
        {
            var directory = Directory.GetCurrentDirectory();
            var directoryName = Path.GetFileName(directory);
            var relativePath = directoryName.StartsWith("net6.0") ? @"../../../" : string.Empty;

            return relativePath;
        }

        private static void ResetDatabase(ArtCollectiveDbContext dbContext, bool shouldDropDatabase = false)
        {
            if (shouldDropDatabase)
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }
            else
            {
                if (dbContext.Database.EnsureCreated())
                {
                    return;
                }

                string disableIntegrityChecksQuery = "EXEC sp_MSforeachtable @command1='ALTER TABLE ? NOCHECK CONSTRAINT ALL'";
                dbContext.Database.ExecuteSqlRaw(disableIntegrityChecksQuery);

                string deleteRowsQuery = "EXEC sp_MSforeachtable @command1='SET QUOTED_IDENTIFIER ON;DELETE FROM ?'";
                dbContext.Database.ExecuteSqlRaw(deleteRowsQuery);

                string enableIntegrityChecksQuery = "EXEC sp_MSforeachtable @command1='ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL'";
                dbContext.Database.ExecuteSqlRaw(enableIntegrityChecksQuery);

                string reseedQuery = "EXEC sp_MSforeachtable @command1='IF OBJECT_ID(''?'') IN (SELECT OBJECT_ID FROM SYS.IDENTITY_COLUMNS) DBCC CHECKIDENT(''?'', RESEED, 0)'";
                dbContext.Database.ExecuteSqlRaw(reseedQuery);
            }
        }
    }
}
