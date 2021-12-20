using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GalleryWebSite.Models.DAL;
using GalleryWebSite.Models.BO;

namespace GalleryWebSite.Models.BLL
{
    public class POABLL
    {
        private POARepository repository = new POARepository();

        public List<PieceOfArt> GetAllByType(int PT1, int PT2)
        {
            return repository.GetAllByType(PT1, PT2);
        }

        public PieceOfArt GetPOAByCN(int CN)
        {
            return repository.GetPOAByCN(CN);
        }

        public List<Perspective> GetAllPOAPerspectivesByCN(int CN)
        {
            return repository.GetAllPOAPerspectivesByCN(CN);
        }

        public int[] GetPoaCount()
        {
            return repository.GetPoaCount();
        }



    }
}