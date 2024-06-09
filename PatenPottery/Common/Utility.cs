using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PatenPottery.Common
{
    public class Utility
    {
        public byte[] ConvertToByteArray(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static string GetModelStateErrors(ModelStateDictionary modelState)
        {
            var errorMessages = modelState.Values.SelectMany(v => v.Errors)
                                                 .Select(e => e.ErrorMessage)
                                                 .ToList();
            return string.Join("<br>", errorMessages);
        }
    }
}
