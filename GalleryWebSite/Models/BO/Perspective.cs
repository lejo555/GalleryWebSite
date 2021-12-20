using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GalleryWebSite.Models.BO
{
    /// <summary>
    /// for each perspective has exactly one drawing
    /// </summary>
    public class Perspective
    {
        //scalar properties
        public int id { get; set; }
        [Required]
        public string PerspectiveCode {get;set;}
        public string PerspectivePath {get;set;}
        
        //Foreign Key
        public int POA_CN { get; set; }
        [ForeignKey("POA_CN")]

        //navigation properties
        public PieceOfArt POA { get; set; }
   
    }
}