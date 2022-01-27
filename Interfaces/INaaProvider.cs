using DatapointAPIPOC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatapointAPIPOC.Interfaces
{
    public interface INaaProvider
    {
        Task<(bool IsSuccess, TileCard1 TileCard1, string ErrorMessage)> GetTileCard1Async(int WidgetID);
        Task<(bool IsSuccess, TileCard1 TileCard1, string ErrorMessage)> GetTileCard1Async(TileRequestObject tileRequestObject);
        Task<(bool IsSuccess, TileCard2 TileCard2, string ErrorMessage)> GetTileCard2Async(TileRequestObject tileRequestObject);
        Task<(bool IsSuccess, TileCard3 TileCard3, string ErrorMessage)> GetTileCard3Async(TileRequestObject tileRequestObject);
        Task<(bool IsSuccess, TileCard4 TileCard4, string ErrorMessage)> GetTileCard4Async(TileRequestObject tileRequestObject);
        Task<(bool IsSuccess, TileCard5 TileCard5, string ErrorMessage)> GetTileCard5Async(TileRequestObject tileRequestObject);

        Task<(bool IsSuccess, TabledData TabledData, string ErrorMessage)> GetTabledDataAsync(TileRequestObject tileRequestObject);
    }
}
