﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Services.Models.Themes
{
    public class PreviewThemesViewModel
    {
        public IEnumerable<PreviewThemeViewModel> PreviewThemes { get; set; }

        public string Input { get; set; }
    }
}
