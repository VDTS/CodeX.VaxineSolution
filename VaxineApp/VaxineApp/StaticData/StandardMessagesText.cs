using System;
using System.Collections.Generic;
using System.Text;

namespace VaxineApp.StaticData
{
    public static class StandardMessagesText
    {
        // Add
        public static string AddTitle { get; set; } = "Added";
        public static string AddBody(string input)
        {
            return $"{input} has been submitted";
        }

        // Delete
        public static string DeleteTitle { get; set; } = null;
        public static string DeleteBody(string input)
        {
            return $"Do you want to remove {input}";
        }

        // Edit
        public static string EditTitle { get; set; } = "Updated";
        public static string EditBody(string input)
        {
            return $"{input} has been updated";
        }

        // Canceled
        public static string CanceledTitle { get; set; } = "Operation Canceled";
        public static string CanceledBody { get; set; } = "Try again later";

        // No data
        public static string NoDataTitle { get; set; } = "No data available";
        public static string NoDataBody { get; set; } = "No data available at the moment, come later or add data";

        // Invalid Data
        public static string InvalidDataTitle { get; set; } = "Invalid Data";
        public static string InvalidDataBody { get; set; } = "Add valid data to required Fields";

        // Features under construction
        public static string FeatureUnderConstructionTitle = "Feature not found";
        public static string FeatureUnderConstructionBody = "This features is under implementation and will be available in future releases. Follow App updates page for more info...";

        // No item Selected
        public static string NoItemSelectedTitle { get; set; } = null;
        public static string NoItemSelectedBody { get; set; } = "Select an item, and come back to operate";
    }
}
