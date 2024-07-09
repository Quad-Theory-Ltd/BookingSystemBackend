using BookingSundorbon.Views.DTOs.ScreenView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSundorbon.Features.Repositories.ScreenRepository
{
    public interface IScreenRepository
    {
        Task CreateScreenAsync(CreateScreenView screen);
        Task CreateScreenFunctionAsync(CreateScreenFunctionView screenFunction);
    }
}
