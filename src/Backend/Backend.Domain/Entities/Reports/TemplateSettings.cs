using System;
using System.Collections.Generic;
using System.Data;
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
        public int BorderRadius { get; set; }

        // Report - Title
        public bool FontTitleBold { get; set; }
        public int FontTitleSize { get; set; }
        public string TitleAlign { get; set; }
        public string FontTitleColor { get; set; }

        // Report - Details
        public string DetailsTextAlign { get; set; }

        // Report - Table
        public int TableWidthSize { get; set; }
        public string TableColor { get; set; }

        // Report - Table - Label
        public int LabelTextSize { get; set; }
        public string LabelTextColor { get; set; }

        // Report - Table - Row
        public int RowTextSize { get; set; }
        public string RowTextColor { get; set; }

        public TemplateSettings() { }

        public TemplateSettings(
            int reportWidh, int borderSize, string borderColor, int borderRadius,
            int titleSize, string titleAlign, bool titleBold, string titleColor,
            string labelTextColor, int labelTextSize,
            string rowTextColor, int rowTextSize,
            string tableColor
            ) 
        {
            ReportWidthSize = reportWidh;

            BorderSize = borderSize;    
            BorderColor = borderColor;
            BorderRadius = borderRadius;

            FontTitleSize = titleSize;
            TitleAlign = titleAlign;
            FontTitleBold = titleBold;
            FontTitleColor = titleColor;

            LabelTextColor = labelTextColor;
            LabelTextSize = labelTextSize;

            RowTextSize = rowTextSize;
            RowTextColor = rowTextColor;

            TableColor = tableColor;
        }
    }
}
