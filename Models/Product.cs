using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RefactorThis.Models
{
    public class Product
    {
        [JsonProperty(PropertyName = "Id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "Price")]
        public decimal Price { get; set; }

        [JsonProperty(PropertyName = "DeliveryPrice")]
        public decimal DeliveryPrice { get; set; }

        [JsonIgnore] public bool IsNew { get; set;  }
    }
}