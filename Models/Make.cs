using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace vega.Models
{
    public class Make
    {
        public int Id { get; set; } 
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public ICollection<Model> Models { get; set; } //una diferencia con List es que este último permite acceder por indice, lo que acá no será necesario    

    public Make()
    {
        Models = new Collection<Model>();
    }
    }
}