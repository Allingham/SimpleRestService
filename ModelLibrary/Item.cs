using System;

namespace ModelLibrary
{
    public class Item
    {
        public int ID{ get; set; }
        public string Name{ get; set; }
        public string Quality{ get; set; }
        public double Quantity{ get; set; }

        public Item(){
        }

        public Item(int id, string name, string quality, double quantity){
            ID = id;
            Name = name;
            Quality = quality;
            Quantity = quantity;
        }

        public override string ToString(){
            return $"ID: {ID}, Name: {Name}, Quality: {Quality}, Quantity: {Quantity}";
        }
    }
}
