using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Reports
{
    public class TemplateSettings
    {
        // Report - General
        public int ReportWidthSize { get; set; }
        public int BorderSize { get; set; }
        public string BorderColor { get; set; }

        // Report - Title
        public bool FontTitleBold { get; set; }
        public int FontTitleSize { get; set; }
        public string TitleAlign { get; set; }

        // Report - Details
        public string DetailsTextAlign { get; set; }

        // Report - Table
        public int ReportTableWidthSize { get; set; }

        // Report - Table - Label
        public int LabelTextSize { get; set; }
        public string LabelTextColor { get; set; }

        // Report - Table - Row
        public int RowTextSize { get; set; }
        public string RowTextColor { get; set; }

        public TemplateSettings() { }

        public TemplateSettings(int titleSize, string titleAlign) 
        {
            FontTitleSize = titleSize;
            TitleAlign = titleAlign;
        }
    }
}
