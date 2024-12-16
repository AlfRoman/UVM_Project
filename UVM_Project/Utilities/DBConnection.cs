

namespace UVM_Project.Utilities
{
    public static class DBConnection
    {
        public static string PathBuilder(string dataBaseName)
        {
            string dbPath = string.Empty;

          
                dbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                dbPath = Path.Combine(dbPath, dataBaseName);
            
         

            return dbPath;
        }
    }
}
