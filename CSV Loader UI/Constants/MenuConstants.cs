namespace CSV_Loader_UI
{
    public static class MenuConstants
    {
        #region Generic
        public const string ResetText = "Press Enter to reset";
        public const string ReturnText = "If you wish to go back press C + Enter";
        #endregion

        #region Find customer
        public const string FindCustomerHeaderText = "Search for customer by Customer reference \n --------------------------------------------------";
        public const string CustomerSearchFieldText = "Ref:";
        public const string SearchResultText = "Search has yielded the following:";
        #endregion

        #region Read file
        public const string ReadFileHeaderText = "Please enter the file path and name of the csv file to read then click Enter";
        public const string ReadFileExampleText = "Example: C:\\CustomerCSVDropoff\\CustomersToLoad.csv";
        public const string FinishedReadingFileText = "Finished! If you wish to load another file press R + Enter. Otherwise just press Enter and you will return to the main menu.";
        #endregion
    }
}
