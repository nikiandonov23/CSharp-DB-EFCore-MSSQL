namespace P01_HospitalDatabase.Data;

public static class Configuration
{
    public const string ConnectionString = "Server=.;Database=HospitalDatabase;Trusted_Connection=True;TrustServerCertificate=True";
}

//NE ZABRAVQI DA SI DOBAVISH USING V DB CONTEXTA :
// using static Common.ApplicationCommonConfiguration; Path-a moje da e razli4en