using LiteCommerce.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface ICountryDAL 
    {
        /// <summary>
        /// get all country
        /// </summary>
        /// <returns></returns>
        List<Country> GetAllCountry();
    }
}
