﻿namespace NetCorePeliculasSeries.Functions
{
    public class AppFunctions
    {
        public string generate_alert(string text, string type_message)
        {
            return "<div class=\"alert alert-" + type_message + " alert-dismissible fade show\" role=\"alert\">" + text + "</div>";
        }
    }
}
