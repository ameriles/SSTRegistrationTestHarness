using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Framework;
using WebService_Tester.ServiceReference1;

namespace SstRegistrationTestHarness
{
    internal class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

    public class SstClient
    {
        public string SSTPID { get; set; }
        public string FedTIN { get; set; }
        public DateTime EffectiveDate { get; set; }
    }

    [TestFixture]
    public class Class1
    {
        public static string CSPID = "CSP000011";
        public static bool IsProduction = false;

        private int? Sequence { get; set; }

        [Test]
        public void ShouldStartManagingRegistration()
        {
            // load these from a file or something.
            var clients = LoadClients();
            var header = GetHeader();
            var documents = MapDocuments(clients);
            header.DocumentCount = documents.Count.ToString();

            var bulkRegister = new BulkRegistrationTransmissionType()
            {
                BulkRegistrationDocument = documents.ToArray(),
                TransmissionHeader = header,
                transmissionVersion = "SST2015V01",
            };

            Console.WriteLine(SerializeObjectToXmlString(bulkRegister));

            try
            {
                var client = new ApiServiceClient();

                client.ClientCredentials.UserName.UserName = "SovosTest";
                client.ClientCredentials.UserName.Password = "Sovos2018";

                var response = client.BulkRegistration(bulkRegister);

                Console.WriteLine(SerializeObjectToXmlString(response));
            }
            catch (FaultException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Code);
                Console.WriteLine(ex.Reason);
            }
        }

        public List<BulkRegistrationDocumentType> MapDocuments(List<SstClient> clients)
        {
            return clients.Select(client => new BulkRegistrationDocumentType()
            {
                DocumentId = GetId(),
                BulkRegistrationHeader = new BulkRegistrationHeaderType()
                {
                    ElectronicPostmark = new BulkRegistrationHeaderTypeElectronicPostmark()
                    {
                        CSPID = CSPID,
                        Value = DateTime.Now
                    },
                    FilingType = BulkRegistrationHeaderTypeFilingType.BulkRegCOU,
                    TIN = new TINType()
                    {
                        FedTIN = client.FedTIN,
                        TypeTIN = TINTypeTypeTIN.FEIN
                    }
                },
                DocumentType = BulkRegistrationDocumentTypeDocumentType.BulkRegistrationCOU,
                Item = new BulkRegistrationCOUType()
                {
                    ActionCode = BulkRegistrationCOUTypeActionCode.C,
                    SSTPID = client.SSTPID,
                    EffectiveDate = DateTime.Today,
                    Items = new []{ new TechModelType()
                    {
                        ItemElementName = ItemChoiceType2.ModelOne,
                        Item = new ModelOneType()
                        {
                            CSPCode = CSPID
                        }
                    } }
                }
            }).ToList();
        }

        public List<SstClient> LoadClients()
        {
            return new List<SstClient>()
            {
                new SstClient()
                {
                    SSTPID = "S00072021",
                    FedTIN = "460000012",
                    EffectiveDate = new DateTime(2018, 5, 1)
                }
            };
        }

        public TransmissionHeaderType GetHeader()
        {
            return new TransmissionHeaderType
            {
                TransmissionId = GetId(),
                Transmitter = new TransmissionHeaderTypeTransmitter()
                {
                    ETIN = "CSP000011"
                }
            };
        }

        public static string SerializeObjectToXmlString(object obj)
        {
            var serializer = new XmlSerializer(obj.GetType());

            using (var output = new Utf8StringWriter())
            using (var writer = new XmlTextWriter(output))
            {
                serializer.Serialize(writer, obj);
                return output.GetStringBuilder().ToString();
            }
        }

        private string GetId()
        {
            if (Sequence == null)
            {
                var nowUtc = DateTime.UtcNow;
                Sequence = (nowUtc.Hour * 60 * 60 + nowUtc.Minute * 60 + nowUtc.Second);
            }

            Sequence++;

            var sequenceString = Sequence.ToString().PadLeft(6, '0');
            sequenceString = sequenceString.Substring(sequenceString.Length - 6);

            var transmitterEtin = CSPID;
            var id =
                $"{transmitterEtin}{DateTime.Today.Year.ToString().Substring(2)}{DateTime.Today.DayOfYear.ToString().PadLeft(3, '0')}{sequenceString}";

            return id;
        }
    }
}
