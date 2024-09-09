﻿using System;
using System.Collections.Generic;

namespace LapShop.Bl
{
    public partial class TbSlider
    {
        public int SliderId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string ImageName { get; set; } = null!;
        public int CurrentState { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual ICollection<TbItem> TbItems { get; set; }
    }
}
