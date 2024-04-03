using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Reports
{
    public class TemplateSettings
    {
        // Title
        public int FontTitleSize { get; set; }
        public string TitleAlign { get; set; }

        public TemplateSettings() { }

        public TemplateSettings(int titleSize, string titleAlign) 
        {
            FontTitleSize = titleSize;
            TitleAlign = titleAlign;
        }
    }
}
