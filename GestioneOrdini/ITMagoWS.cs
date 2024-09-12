using GestioneOrdini.TbServices;
using System;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Xml;

namespace GestioneOrdini
{
    /// <summary>
    /// Classe integrazione webService di mago
    /// </summary>
    public class ITMago4WS
    {
        private string sCompanyName;
        private string sServerName;
        private string sMagoPort;
        private string sInstanceName;
        private string sLoginName;
        private string sPassword;
        private string sProducerKey;
        private const string attributeId = "id";

        //private const string attributeName = "name";
        //private const string attributeModalState = "modalstate";
        private const string processesQuery = "Processes/Process";

        private DateTime createTbLoaderDate = DateTime.Today;

        private string authenticationToken = string.Empty;

        /// <summary>
        /// Authentication Token, valorizzato dopo LoginMago
        /// </summary>
        public string AuthenticationToken
        {
            get { return authenticationToken; }
        }

        /// <summary>
        /// True quando è login, false altrimenti
        /// </summary>
        public bool LoginEffettuato
        {
            get { return authenticationToken != string.Empty && authenticationToken != null; }
        }

        /// <summary>
        /// TbPort, porta del TbLoader
        /// </summary>
        public int TbPort
        {
            get
            {
                return tbPort;
            }
        }

        private int tbPort;
        private string easyLookToken;
        private string serverMagicLink;

        /// <summary>
        /// Istanza LogingManager
        /// </summary>
        public LoginManager.MicroareaLoginManager aLogMng;

        /// <summary>
        /// Istanza LockManager
        /// </summary>
        public LockManager.MicroareaLockManager aLockMng;

        /// <summary>
        /// Istanza TbService
        /// </summary>
        public TbServices.TbServices aTbService;

        /// <summary>
        /// Istanza nuovo oggetto ItechWebService con parametri obbligatori
        /// </summary>
        /// <param name="sCompanyName">Nome azienda</param>
        /// <param name="sServerName">Indirizzo server mago</param>
        /// <param name="sMagoPort">Porta server mago</param>
        /// <param name="sInstanceName">Istanza server mago</param>
        /// <param name="sLoginName">Utente per login in mago</param>
        /// <param name="sPassword">Password per login in mago</param>
        /// <param name="sProducerKey">producer key</param>
        public ITMago4WS(string sCompanyName, string sServerName, string sMagoPort, string sInstanceName, string sLoginName, string sPassword, string sProducerKey)
        {
            tbPort = 0;
            this.sCompanyName = sCompanyName; //AziendaDemo
            this.sServerName = sServerName; //localhost
            this.sMagoPort = sMagoPort; //80
            this.sInstanceName = sInstanceName; //mago4
            this.sLoginName = sLoginName; //sa
            this.sPassword = sPassword; //itech
            this.sProducerKey = sProducerKey; //GestioneOrdini
        }

        /// <summary>
        /// Inizializza tutte le variabili a vuoto (NON USARE)
        /// </summary>
        public void Inizializza()
        {
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            aLogMng = new LoginManager.MicroareaLoginManager();
            aLockMng = new LockManager.MicroareaLockManager();
            aTbService = new TbServices.TbServices();
            authenticationToken = string.Empty;
        }

        private bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        /// <summary>
        /// Termina il tbLoader lato server
        /// </summary>
        public void TerminateTBLoader()
        {
            bool stopped/* = true*/;
            string istantiatedList = aTbService.GetTbLoaderInstantiatedListXML(authenticationToken.ToString());

            if (string.IsNullOrEmpty(istantiatedList))
                return;

            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(istantiatedList);
            }
            catch
            {
                return;
            }

            XmlNodeList processList = xmlDoc.SelectNodes(processesQuery);

            if (processList == null || processList.Count == 0)
                return;

            foreach (XmlElement proc in processList)
            {
                string procID = proc.GetAttribute(attributeId);

                try
                {
                    aLogMng.LogOff(authenticationToken.ToString());
                    aTbService.CloseTB(authenticationToken.ToString());
                    aLockMng.Dispose();
                    stopped = aTbService.StopProcess(Int32.Parse(procID), authenticationToken.ToString());
                    authenticationToken = string.Empty;
                }
                catch (Exception)
                {
                    //eccezione
                    return;
                }
            }
            authenticationToken = string.Empty;
        }

        /// <summary>
        /// Effettua il login a mago, inizializza e valorizza tutte le istanza dei ws
        /// </summary>
        /// <param name="nTentativi"></param>
        /// <returns></returns>
        public int LoginMago(int nTentativi)
        {
            Inizializza();
            authenticationToken = string.Empty;

            aLockMng.Url = string.Format("http://{0}:{1}/{2}/{3}/{4}.asmx", sServerName, sMagoPort, sInstanceName, "LockManager", "LockManager");
            aLogMng.Url = string.Format("http://{0}:{1}/{2}/{3}/{4}.asmx", sServerName, sMagoPort, sInstanceName, "LoginManager", "LoginManager");
            aTbService.Url = string.Format("http://{0}:{1}/{2}/{3}/{4}.asmx", sServerName, sMagoPort, sInstanceName, "TbServices", "TbServices");
            // fa alcuni tentativi di accesso nel caso ci possano essere dei ritardi dalla parte server
            do
            {
                try
                {
                    int aRes = aLogMng.LoginCompact(ref sLoginName, ref sCompanyName, sPassword, sProducerKey, false, out authenticationToken);
                    //if (bTraceLog)
                    //    WriteLog("Param: " + sLoginName + " (" + authenticationToken + ")");
                    if (authenticationToken != string.Empty)
                    {
                        if (createTbLoaderDate != DateTime.Today)
                        {
                            aLockMng.Dispose();
                            aLogMng.LogOff(authenticationToken);
                            aTbService.CloseTB(authenticationToken);
                            authenticationToken = string.Empty;
                            createTbLoaderDate = DateTime.Today;
                        }
                        tbPort = aTbService.CreateTBTagged(authenticationToken, createTbLoaderDate, true, sProducerKey, out easyLookToken, out serverMagicLink);

                        aTbService.SetForceApplicationDate(true);
                        if (tbPort < 10000)
                        {
                            //Log.Error($"{MainActivity.gltag} - WS", "CreateTB:" + TbMessage(tbPort));
                        }
                        else
                        {

                        }
                    }
                    else
                        tbPort = aRes;
                }
                catch (System.Web.Services.Protocols.SoapException)
                {
                    //eccezione
                    //if (bTraceLog)
                    //   WriteLog("LIN:" + err.Message.ToString());
                }
                if (tbPort == -4 || tbPort == -1)  // Non è partito TBLoader o ho avuto un errore di AuthToken
                    nTentativi--;
                else
                    nTentativi = 0;
            } while (tbPort < 0 && nTentativi > 0);

            return tbPort;
        }

        /// <summary>
        /// Effettua il logout da mago
        /// </summary>
        public void LogoutMago()
        {
            try
            {
                aLogMng.LogOff(authenticationToken.ToString());
                aTbService.CloseTB(authenticationToken.ToString());
                aLockMng.Dispose();
                authenticationToken = string.Empty;
                easyLookToken = string.Empty;
            }
            catch (System.Web.Services.Protocols.SoapException)
            {
                //eccezione
                //if (bTraceLog)
                //    WriteLog("LOU:" + err.Message.ToString());
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Avviso", "alert('" + err.Message.ToString() + "');", true);
                //throw (err);
            }
        }

        /// <summary>
        /// Restituisce l'errore di login con mago sotto forma di stringa
        /// </summary>
        /// <param name="nErrore">valore tbport restituito minore 10000 </param>
        /// <returns></returns>
        public string TbMessage(int nErrore)
        {
            string sResult = nErrore switch
            {
                -1 => "Token di autenticazione non valido",
                -2 => "Login fallita per l'utente EasyLookSystem",
                -3 => "Errore avviando TbLoader",
                -4 => "Inizializzazione TbLoader fallita",
                -5 => "Non è possibile impostare la data sulla nuova istanza di TBLoader",
                1 => "LoginManager ha fallito la connessione al database di sistema.",
                2 => "Failure in PathFinder initialization.",
                3 => "LoginManager has not ben able to initialize Activation Manager.",
                4 => "Non esiste la licenza per questa funzionalità.",
                6 => "Lettura della tabella delle licenze fallita.",
                7 => "returns ID company value stored in MSD_CompanyLogins table belonging to system database.",
                8 => "Utente già connesso su un'azienda diversa da quella specificata.",
                9 => "Utente già connesso al sistema.",
                10 => "Licenze utente terminate.",
                11 => "Impossibile associare l'utente alla licenza.",
                12 => "Token di autenticazione vuoto.",
                13 => "Utente inesistente nel database di sistema! Verifica di aver inserito correttamente la password.",
                14 => "The process not allows a simultaneous login",
                15 => "Error in change password.",
                16 => "Password scaduta.",
                17 => "Password  troppo corta.",
                18 => "Il database aziendale è in uso da un altro utente.",
                19 => "L'utente deve cambiare password, ma la sua configurazione non lo consente.",
                20 => "Errore di login generico.",
                21 => "L'azienda selezionata non esiste nel database di  sistema.",
                22 => "System database not contains information about the provider specified for the company.",
                23 => "Impossible to read from system database the connection parameters for the company.",
                24 => "LoginManagerWrapper Uninitialized.",
                25 => "LoginManager not logged.",
                26 => "LoginManager initializing error.",
                27 => "LoginManager not initializated.",
                29 => "The company database does not exists or it is not possible connecting to it. It must be created with the Administration Console.",
                30 => "The company database table does not exists. They must be created with the Administration Console.",
                31 => "The company database is not supported by application configuration.",
                32 => "User can not access to Web applications.",
                33 => "User can not access to SmartClient applications.",
                34 => "EasyLook article is not activated, is not usabled.",
                35 => "Utente bloccato! Deve essere sbloccato dall'amministratore di sistema.",
                36 => "Unable to change password many times during the same day.",
                37 => "Integrated security users can not use this functionality.",
                38 => "The version of company database is not compatible with program in use.",
                39 => "Mago non è attivato!",
                40 => "The selected company is not available because it violate the Standard Edition boundaries.",
                41 => "MagicDocuments module is not usable, it lacks CAL.",
                42 => "WebUserAlreadyLogged: The user is already connected to the system with another application.",
                43 => "In the system database there are more users assigned than the CAL number of the article.",
                _ => "Errore " + nErrore.ToString() + " non definito!",
            };
            return sResult;
        }

        /// <summary>
        /// Metodo che esegue l'analisi dell'XML restituito come errore da ML
        /// </summary>
        /// <param name="DocumentName">Nome documento (per estrarre l'errore)</param>
        /// <param name="sXMLOut">XML restituito da ML</param>
        /// <returns></returns>
        public static string AnalizzaXMLErroreML(string DocumentName, string sXMLOut)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(sXMLOut);
            XmlNamespaceManager nsmSchema = new XmlNamespaceManager(xDoc.NameTable);
            nsmSchema.AddNamespace(xDoc.DocumentElement.Prefix, xDoc.DocumentElement.NamespaceURI);
            XmlElement itemNode = (XmlElement)xDoc.SelectSingleNode($"/maxs:{DocumentName}/maxs:Diagnostic/maxs:Errors/maxs:Error/maxs:Message", nsmSchema);
            return itemNode.InnerText;
        }
    }
}