using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simile
{
    /// <summary>
    /// It holds 
    /// </summary>
   public class TruncateMaster : ICollection<TruncateMaster>
    {
       public Int16 Order
       { get; set; }
       public Symbols Symbol
       {
           get;
           set;
       }
       public enum Symbols
       {
           Unknown = 0,
           Numeric = 1,
           UnderscoreHyphen = 2,
           Hyphen = 3
       };     

       TruncateMaster truncateList;
       public TruncateMaster()
       {
          
         
       }
        

       public void Add(TruncateMaster item)
       {
           if (this.truncateList != null)
           {
               this.truncateList.Add(item);
           }
           else
           {
               throw new Exception("Array not initialized");
           }
       }

       public void Clear()
       {
           if (this.truncateList != null)
           {
               this.truncateList.Clear();
           }
           else
           {
               throw new Exception("Array not initialized");
           }
       }

       public bool Contains(TruncateMaster item)
       {
           throw new NotImplementedException();
       }

       public void CopyTo(TruncateMaster[] array, int arrayIndex)
       {
           throw new NotImplementedException();
       }

       public int Count
       {
           get {
               if (this.truncateList != null)
               {
                 return  this.truncateList.Count();
               }
               else
               {
                   throw new Exception("Array not initialized");
               }
           }
       }

       public bool IsReadOnly
       {
           get { throw new NotImplementedException(); }
       }

       public bool Remove(TruncateMaster item)
       {
           throw new NotImplementedException();
       }

       public IEnumerator<TruncateMaster> GetEnumerator()
       {
           throw new NotImplementedException();
       }

       System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
       {
           if (this.truncateList != null)
           {
               return this.truncateList.GetEnumerator();
           }
           else
           {
               throw new Exception("Array not initialized");
           }
       }

       private void CreateSymbolList()
       {
           this.truncateList = new TruncateMaster();
           this.truncateList.Add(new TruncateMaster(){Order = 1, Symbol = Symbols.Numeric});
           this.truncateList.Add(new TruncateMaster() { Order = 1, Symbol = Symbols.UnderscoreHyphen });
       }

       public String GetNewName(String value)
       {
           String newName = value;
          

           Boolean canTrim = false;


           canTrim = Char.IsDigit( Convert.ToChar((StretchedMethod.GetFirstCharacter(value))));
           canTrim = !canTrim ?  
            
            
           
           

           switch (firstValue)
           {
               case '.':
                   value = "_";
                 
                   break;
               case '_':
                   value = "-";
                   break;
               case '-':
                   break;
               case ' ':
                   break;
               case '1':
                   break;
               default:
                   break;
           }
           return symbolValue;
       
       }

       public Symbols GetSymbolValue(int order)
       {
           String value = "";
           switch (order)
           {
               case 1:
                   value = "_";
                   break;
               case 2:
                   value = "-";
                   break;
               default:
                   break;
           }
           return value;

       }

      
    }
}
