using Poetizando.Entidade;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace Poetizando.Negocio.Help
{
    public class RoboAutores
    {
        private IList<Autor> autoresSemImagem = new List<Autor>();
        private IList<Autor> autoresSemInformacao   = new List<Autor>();
        private IList<Autor> autoresSalvos = new List<Autor>();
        private int tentativasInformacao = 0;
        private int tentativasImagem = 0;
        private string ip = "189.60.129.231";

        public void AjustarInformacoes()
        {
            var boAutor = new AutorBusiness();

            var autoresParaAjustar = boAutor.Filtrar().Where(x => x.Imagem == null  && x.Nome != "Desconhecido").OrderBy(x => x.Nome).ToList();
            var total = autoresParaAjustar.Count;
            foreach (var autor in autoresParaAjustar)
            {                               
                if (RecuperarImagens(autor))
                {
                    boAutor.Alterar(autor);
                    Trace.WriteLine("Salvando autor:" + autor.Nome);
                    autoresSalvos.Add(autor);
                }
                
                Trace.WriteLine("Autores sem imagem:" + autoresSemImagem.Count);
                Trace.WriteLine("Autores sem informacao:" + autoresSemInformacao.Count);

                total--;
                tentativasImagem = tentativasInformacao = 0;
                Trace.WriteLine("Faltam:" + total);
                
                System.Threading.Thread.Sleep(20000);
            }

            Trace.WriteLine("");
            Trace.WriteLine("");
            Trace.WriteLine("======== LISTANDO AUTORES SEM IMAGEM =========");
            foreach (var autor in autoresSemImagem)
                Trace.WriteLine(string.Format("[{0}] [{1}]", autor.Id, autor.Nome));

            Trace.WriteLine("");
            Trace.WriteLine("");
            Trace.WriteLine("======== LISTANDO AUTORES SEM INFORMACAO =========");
            foreach (var autor in autoresSemInformacao)
                Trace.WriteLine(string.Format("[{0}] [{1}]", autor.Id, autor.Nome));

            Trace.WriteLine("");
            Trace.WriteLine("");
            Trace.WriteLine("======== LISTANDO AUTORES =========");
            foreach (var autor in autoresSalvos)
                Trace.WriteLine(string.Format("[{0}] [{1}]", autor.Id, autor.Nome));

        }

        public bool RecuperarInformacoes(Autor autor)
        {
            try
            {
                tentativasInformacao++;

                var url = String.Format("https://ajax.googleapis.com/ajax/services/search/web?v=1.0&q={0}&cr=countryBR&hl=pt-BR&userip={1}", autor.Nome, ip);

                Trace.WriteLine(String.Format("Recuperando informações [{0}]", url));

                var json = RecuperarJSON(url);

                autor.Wiki = json["responseData"]["results"][0]["url"];
                autor.Destaque = true;
                //autor.Descricao = json["responseData"]["results"][0]["content"];

                return true;                
            }
            catch
            {
                if (tentativasInformacao < 10)
                {
                    Trace.WriteLine("Google barrou o acesso, aguardando...");
                    System.Threading.Thread.Sleep(120000);
                    return RecuperarInformacoes(autor);
                }

                autoresSemInformacao.Add(autor);

                return false;
            }
        }

        public bool RecuperarImagens(Autor autor)
        {
            try
            {
                tentativasImagem++;

                var url = String.Format("https://ajax.googleapis.com/ajax/services/search/images?v=1.0&q={0}&as_filetype=jpg&imgsz=medium|large|xlarge&imgtype=face&cr=countryBR&hl=pt-BR&userip={1}", autor.Nome, ip);

                Trace.WriteLine(String.Format("Recuperando imagem [{0}]", url));

                var json  = RecuperarJSON(url);

                var imagem = json["responseData"]["results"][0]["unescapedUrl"];

                var bytes = RecuperarBytes(imagem);

                FileStream fs = new FileStream(@"C:\Users\Diogo\Desktop\Teste\" + autor.Id + ".jpg", FileMode.Create);
                BinaryWriter w = new BinaryWriter(fs);
                try
                {
                    w.Write(bytes);
                }
                finally
                {
                    fs.Close();
                    w.Close();
                }

                autor.Imagem = autor.Id + ".jpg";

                return true;
            }
            catch
            {
                if (tentativasImagem < 10)
                {
                    Trace.WriteLine("Google barrou o acesso, aguardando...");
                    System.Threading.Thread.Sleep(120000);
                    return RecuperarImagens(autor);
                }

                autoresSemImagem.Add(autor);

                return false;
            }
        }

        private dynamic RecuperarJSON(string url)
        {
            return new JavaScriptSerializer().Deserialize<dynamic>(RecuperarConteudo(url));
        }

        public byte[] RecuperarBytes(string url)
        {            
            System.Net.HttpWebRequest myReq = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            System.Net.WebResponse myResp = myReq.GetResponse();

            byte[] b = null;
            using (Stream stream = myResp.GetResponseStream())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        byte[] buf = new byte[1024];
                        count = stream.Read(buf, 0, 1024);
                        ms.Write(buf, 0, count);
                    } while (stream.CanRead && count > 0);
                    b = ms.ToArray();
                }
            }

            myResp.Close();
            return b;
        }

        private string RecuperarConteudo(string url)
        {

            StreamReader reader = new StreamReader(RecuperarStream(url), Encoding.GetEncoding(1252));
            string resultado = reader.ReadToEnd();
            return resultado;
        }

        private Stream RecuperarStream(string url)
        {
            if (String.IsNullOrEmpty(url))
                throw new ArgumentNullException("Url");

            var cookies = new CookieContainer();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = Guid.NewGuid().ToString();
            request.Method = "GET";
            request.Accept = "text/html";
            request.CookieContainer = cookies;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            return response.GetResponseStream();
        }
        
    }
}

