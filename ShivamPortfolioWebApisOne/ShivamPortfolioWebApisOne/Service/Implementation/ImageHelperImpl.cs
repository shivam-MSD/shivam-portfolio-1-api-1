using ShivamPortfolioWebApisOne.Constants;
using ShivamPortfolioWebApisOne.Dtos;
using System.Reflection;
using ShivamPortfolioWebApisOne.Service;

namespace ShivamPortfolioWebApisOne.Service.Implementation
{
    public class ImageHelperImpl<TDto> : IImageHelper<TDto> where TDto : IDtoWithFilePath
    {
        public Dictionary<string, byte[]> AddOrUpdateImageWithPriority(Dictionary<string, byte[]> priorityImageDict, TDto dto)
        {
            string filePath = dto.ImagePath;

            //if (filePath != null)
            //{
            //    try
            //    {
            //        if (File.Exists(dto.ImagePath) && Constants.Constants.supportedImageExtensions.Contains(Path.GetExtension(dto.ImagePath)))
            //        {
                        //byte[] byteContent = File.ReadAllBytes(dto.ImagePath);
                        byte[] byteContent = GetImageBytes(filePath);
                        if(byteContent == null)
                        {
                            return null;
                        }
                        if (priorityImageDict == null)
                        {
                            priorityImageDict = new Dictionary<string, byte[]>();
                            priorityImageDict.Add(dto.ImagePriority.ToString(), byteContent);

                            return priorityImageDict;
                        }
                        else if (priorityImageDict.ContainsKey(dto.ImagePriority.ToString()))
                        {
                            priorityImageDict[dto.ImagePriority.ToString()] = byteContent;
                        }
                        else
                        {
                            priorityImageDict.Add(dto.ImagePriority.ToString(), byteContent);
                        }
                        return priorityImageDict;
            //        }
            //        else
            //        {
            //            throw new FileNotFoundException("Image not found or File type is not an image type. Please provide an image file path only.");
            //        }
            //    }
            //    catch
            //    {
            //        throw new Exception("Something went wrong.....\nImage not found or File type is not an image type. Please provide an image file path only.");
            //    }
            //}
            //return null;
        }

        public byte[] GetImageBytes(string filePath)
        {
            if(filePath != null)
            {
                try
                {
                    if (File.Exists(filePath) && Constants.Constants.supportedImageExtensions.Contains(Path.GetExtension(filePath)))
                    {
                        return File.ReadAllBytes(filePath);
                    }
                    else
                    {
                        throw new FileNotFoundException("Image not found or File type is not an image type. Please provide an image file path only.");
                    }
                }
                catch
                {
                    throw new Exception("Something went wrong.....\nImage not found or File type is not an image type. Please provide an image file path only.");
                }
            }
            return null;
        }
    }
}
