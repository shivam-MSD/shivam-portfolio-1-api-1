using System.Collections.Generic;
using ShivamPortfolioWebApisOne.Dtos;

namespace ShivamPortfolioWebApisOne.Service
{

    public interface IImageHelper<TDto> where TDto : IDtoWithFilePath
    {
        Dictionary<string, byte[]> AddOrUpdateImageWithPriority(Dictionary<string, byte[]> priorityImageDict, TDto dto);
        byte[] GetImageBytes(string filePath);

    }
}
