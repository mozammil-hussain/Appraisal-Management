//using appraisalmanagentsystem.interfaces;
//using system.net.mail;
//using system.net;

//namespace appraisalmanagentsystem.interfaceimpl
//{
//    public class emailserviceimpl : iemailservice
//    {
//        private readonly iconfiguration _configuration;

//        public emailserviceimpl(iconfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public async void sendemail(string to, string subject, string body)
//        {
//            var smtpsettings = _configuration.getsection("smtpsettings");

//            var smtpclient = new smtpclient
//            {
//                host = smtpsettings["host"],
//                port = int.parse(smtpsettings["port"]),
//                enablessl = true,
//                credentials = new networkcredential(smtpsettings["username"], smtpsettings["password"])
//            };

//            using (var message = new mailmessage(smtpsettings["username"], to)
//            {
//                subject = subject,
//                body = body,
//                isbodyhtml = true
//            })
//            {
//                try
//                {
//                    smtpclient.send(message);
//                }
//                catch (exception ex)
//                {

//                }
//            }
//        }

//    }
//}
