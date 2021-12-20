using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalleryWebSite.Models.BO
{

    //ביאור סימולי קטגוריות מעודכן נמצא בקובץ אקסל
    //CategoriesSymbols.xlsx
    
    /// <summary>
    /// each piece of art can have more than one perspective
    /// </summary>

    public class PieceOfArt
    {
        // scalar properties
        //public int id { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int CN { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        [Required]
        public int PT { get; set; }
        public string POAPath { get; set; }

        //navigation properties
        public List<Perspective> Perspectives { get; set; }
    }
}