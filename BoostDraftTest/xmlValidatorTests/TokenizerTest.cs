using xmlValidator;

namespace xmlValidatorTests
{
    public class Tests
    {

        [Test]
        public void TokenizeTest()
        {
            //Arrange
            string xml = "<Code>hello</Code>";
            List<string> tokens = new List<string>();
            var validator = new XmlValidator();

            //Act
            List<string> expected = Tokenizer(xml);
           

            //Assert
            Assert.AreEqual();

          
        }

        [Test]
        public void DetermineNestOrderTest(List<string> tokens)
        {
            //TODO
        }

        [Test]
        public void SelfClosingCheckTest(string token)
        {
            //TODO
        }

        [Test]
        public void DetermineXmlTest(string token)
        {
            //TODO
        }
    }
}