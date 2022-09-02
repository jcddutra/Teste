

using System;
using static System.Console;
namespace teste2
{
    class Program
    {
        
    static void Main(string[] args)
        {   
        
            
            var numnegocio="";
            var dataref="";
            string mensagem;
            string value;
            string  clientSector;
            string  NextPaymentDate;
            List<Portifolio> listaPortifolio = new List<Portifolio>();

            ConsoleKeyInfo cki;
            Console.Clear(); 
            WriteLine("-------------------------------------------------------------------------------------------");
            WriteLine("-----------------------------Minha Aplicação-----------------------------------------------");
            WriteLine("-------------------------------------------------------------------------------------------");
            WriteLine("\n\n\n\n");

//Digitação da data de referência
 ////////////////////////////////////////////////////////           
           do
            {
             WriteLine("Digite a Data Referência: ");
                dataref = ReadLine();  
                   
                if(ValidaData(dataref))
                   break;
                 WriteLine("Data invalida formato MM/DD/YYYY  (ESC) Sair" );
                 cki = Console.ReadKey();  
             }  while (cki.Key != ConsoleKey.Escape);
            
      
//Digitação Número de Negocíos
 ////////////////////////////////////////////////////////         
            do
            {
                WriteLine("Número de Negocíos: ");
                numnegocio = ReadLine();
                ValidaCampo(numnegocio,"N",out mensagem);
                if(mensagem=="")
                   break;
                 WriteLine("Campo invalido valor numérico (ESC) Sair");
                 cki = Console.ReadKey();  
             }  while (cki.Key != ConsoleKey.Escape);

//Inicio igitação dos itens
 ////////////////////////////////////////////////////////    

            int contador=1;
            do
            {
                
                WriteLine("Linha: " + Convert.ToString(contador));
                WriteLine("valor da negociação/Setor do cliente/Data próximo Pagamento Pendente");
             
                var input="";
                input = ReadLine();

                WriteLine("------------------------------------------------------------------------------------------");
              
//valida se tem erro no campo input
               SeparaInput(input,out value,out clientSector,out NextPaymentDate,out mensagem);

               if(mensagem!="")               
                    WriteLine(mensagem);

               else if(!ValidaData(NextPaymentDate))
                     WriteLine("Data invalida");

               else if(clientSector != "Private" && clientSector != "Public")
                         WriteLine("Setor invalido");
               else
               {
               // Gravação da Lista
               ////////////////////////////////
                Portifolio portifolio = new Portifolio();
                portifolio.Dataref=dataref;
                portifolio.Numnegocio=Convert.ToInt32(numnegocio);
                portifolio.Value=Convert.ToDouble(value);
                portifolio.ClientSector=clientSector;
                portifolio.NextPaymentDate=NextPaymentDate;
                listaPortifolio.Add(portifolio);
                contador ++;
               }
           
       
                WriteLine("Tecle Enter para continuar (ESC) Sair");
                cki = Console.ReadKey();   
                Clear();
                if (cki.Key == ConsoleKey.Escape)
                   break;

               if (contador>Convert.ToInt32(numnegocio))
                   break;
              
               
            }  while (cki.Key != ConsoleKey.Escape );

        //relatório Output  
        ////////////////////// 
            Clear();
            WriteLine("=================OutPut===================");
            foreach(Portifolio portifolio in listaPortifolio){
                   int totalDias = (Convert.ToDateTime(ConvertData(portifolio.Dataref)) - Convert.ToDateTime(ConvertData(portifolio.NextPaymentDate))).Days;
                    if(totalDias > 30)
                       Console.WriteLine("EXPIRED");
                    else if(portifolio.Value > 1000000 && portifolio.ClientSector == "Private")
                      Console.WriteLine("HIGHRISK");
                    else if(portifolio.Value > 1000000 && portifolio.ClientSector == "Public")
                    Console.WriteLine("MEDIUMRISK");
                    else
                     Console.WriteLine("");
             }
        }           
 
    

#region Funcão ValidaCampo
    

  public static void  ValidaCampo(string input, string type, out string mensagem)
    {  
        mensagem="";
        double valueaux=0;
        DateTime NextPaymentDateaux;
         try
         {
            if (type=="D")
            {
            string[] words = input.Split('/');
              
              NextPaymentDateaux=Convert.ToDateTime(input + "00:00:00");
             

            }else {
            valueaux=Convert.ToDouble(input);
            }

         }
          catch
          {
        
            mensagem="Conteudo do campo invalido.";
          }
     } 
 #endregion
#region Funcão ValidaData


  public static string ConvertData(string data)
  {
    string dia, mes, ano;
    string aux;
    aux = data;
                       
            mes = aux.Substring(0, 2);
            dia = aux.Substring(3, 2);
            ano = aux.Substring(6, 4);
            data=dia + "/" + mes + "/" + ano;
            return data;
  }
  public static Boolean ValidaData(string data)
  { 
    int dia, mes, ano;
    string aux;
    aux = data;
           if(data=="")
              return false;
            mes = Convert.ToInt32(aux.Substring(0, 2));
            dia = Convert.ToInt32(aux.Substring(3, 2));
            ano = Convert.ToInt32(aux.Substring(6, 4));

           
            if   (dia < 1 || dia > 31)      
                 return false;
            
            if (mes < 1 || mes > 12)     
                 return false;
            if ((dia == 29) && (mes == 2))
            {
              if (!(Convert.ToInt32(ano) % 4 == 0) && (Convert.ToInt32(ano) % 100 != 0))
              {
                  return false;
              }
     }       return true;
  }
   #endregion
#region Funcão SeparaInput    

    public static void  SeparaInput(string input ,out string value, out string clientSector, out string NextPaymentDate, out string mensagem)
    {   mensagem="";
        double valueaux=0;
        try{
         string[] words = input.Split(' ');
         value=words[0];
         valueaux=Convert.ToDouble(words[0]);  
         clientSector=words[1];           
         NextPaymentDate=words[2];            
        }
          catch
          {
            value="";
            clientSector="";
            NextPaymentDate="";
            mensagem="Conteudo do campo invalido.";
          }
    
    }
  #endregion
  
     }

    
}

