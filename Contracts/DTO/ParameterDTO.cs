using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTO
{
    public class ParameterDTO
    {
        public int Id { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public int ShoeSize { get; set; }
        public int ClothingSize { get; set; }
        public int GasMaskSize { get; set; }
        public int HeadCircumference { get; set; }
        public int PersonId { get; set; }
    }
}
