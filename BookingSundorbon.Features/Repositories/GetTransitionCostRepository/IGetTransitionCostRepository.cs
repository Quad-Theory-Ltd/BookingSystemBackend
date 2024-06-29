using BookingSundorbon.Views.DTOs.GetTransitionCostView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.GetTransitionCostRepository
{
    public interface IGetTransitionCostRepository
    {
        //Task <decimal> GetTransitionCostWithOutVat(decimal parcellLengt, decimal parcellWidth, decimal parcelHeight, decimal parcellWeight, string cargoType, bool isExtralPackaging, bool isFragiItem );
        Task<decimal> GetTransitionCostWithoutVat(GetTransitionCostView transitionCostView);
    }
}
