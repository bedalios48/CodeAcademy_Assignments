﻿using System;
using System.Collections.Generic;

namespace Assignments09_2.Domain.Models.Database
{
    public partial class Track
    {
        public Track()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
            Playlists = new HashSet<Playlist>();
        }

        public long TrackId { get; set; }
        public string Name { get; set; } = null!;
        public long? AlbumId { get; set; }
        public long MediaTypeId { get; set; }
        public long? GenreId { get; set; }
        public string? Composer { get; set; }
        public long Milliseconds { get; set; }
        public long? Bytes { get; set; }
        public byte[] UnitPrice { get; set; } = null!;
        public string Status { get; set; } = "Active";

        public virtual Album? Album { get; set; }
        public virtual Genre? Genre { get; set; }
        public virtual MediaType MediaType { get; set; } = null!;
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }

        public virtual ICollection<Playlist> Playlists { get; set; }
        public virtual decimal Price => decimal.Parse(new string(UnitPrice.Select(x => Convert.ToChar(x)).ToArray()));
    }
}
