using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GalleryWebSite.Models.BO;

namespace GalleryWebSite.Models.DAL
{
    public class POARepository
    {
        public GalleryDBContext galleryDBContext = new GalleryDBContext();

        /// <summary>
        /// returns all poa's by type
        /// </summary>
        /// <param name="PT1">first type</param>
        /// <param name="PT2">second type- optional</param>
        /// <returns></returns>
        public List<PieceOfArt> GetAllByType(int PT1, int PT2)
        {
            if (PT2 == 0)
            {
                return galleryDBContext.PiecesOfArt.Include("Perspectives").Where(item => item.PT == PT1).ToList();
            }
            else
            {
                return galleryDBContext.PiecesOfArt.Include("Perspectives").Where(item => (item.PT == PT1 || item.PT == PT2)).ToList();
            }
        }

        /// <summary>
        /// retruns poa by its catalog number
        /// </summary>
        /// <param name="CN"></param>
        /// <returns></returns>
        public PieceOfArt GetPOAByCN(int CN)
        {
            return galleryDBContext.PiecesOfArt.Where(item => item.CN == CN).FirstOrDefault();
        }

        /// <summary>
        /// returns all poa's perspectives by catalog number
        /// </summary>
        /// <param name="CN"></param>
        /// <returns></returns>
        public List<Perspective> GetAllPOAPerspectivesByCN(int CN)
        {
            return galleryDBContext.Perspectives.Where(item => item.POA.CN == CN).ToList();
        }

        /// <summary>
        /// returns array of mosaics/stain glass/fusing poa's amount 
        /// <returns></returns>
        public int[] GetPoaCount()
        {
            int[] countList = new int[3];
            countList[0] = galleryDBContext.PiecesOfArt.Where(item => item.PT % 10 == 1).Count();//sg
            countList[1] = galleryDBContext.PiecesOfArt.Where(item => item.PT % 10 == 2).Count();//ms
            countList[2] = galleryDBContext.PiecesOfArt.Where(item => item.PT % 10 == 3).Count();//fs
            return countList;
        }


    }
}