using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using GalleryWebSite.Models.BO;

namespace GalleryWebSite.Models.DAL
{
    public class GalleryDBContext : DbContext
    {
        public DbSet<PieceOfArt> PiecesOfArt { get; set; }
        public DbSet<Perspective> Perspectives { get; set; }

    }
}