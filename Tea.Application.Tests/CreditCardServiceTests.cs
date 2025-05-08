using Tea.Application;

namespace Tea.Application.Tests
{
    public class CreditCardServiceTests
    {
        //powinno akceptowac typ tekstowy 13-19 znakow wliczajac myslniki/spacje pomiedzy i zgodny z algorytmem luhna
        //made with luhn algorithm calculator https://simplycalc.com/luhn-calculate.php

        public class CreditCardServiceTest
        {
            [Theory]
            [InlineData("3497 7965 8312 797")] //prawidlowy, spacje; 15 znakow
            [InlineData("345-470-784-783-010")] //prawidlowy, myslniki; 15 znakow 
            [InlineData("4547078478308668673")] //19 znakow
            [InlineData("4532289052803")] // 13 znakow
            [InlineData("4-5-3---2289052803")] // 18 znakow, wiele myslnik�w 
            [InlineData("453228905 -  -2803")] //13 znakow, myslniki i spacje
            [InlineData("0000000000000")] // algorytm luhna bledna druga cyfra
            [InlineData("1020202010101")] // algorytm luhna poprawny kod bez alternate
            [InlineData("2040404020202")] // algorytm luhna poprawny kod bez alternate
            [InlineData("0001010101010")] // algorytm luhna poprawny kod samo alternate

            public void ValidateCreditCardNumber_ShouldReturnTrue(string cardNumber)
            {
                var credit = new CreditCardService();
                bool result = credit.ValidateCard(cardNumber);
                Assert.True(result);
            }

            [Theory]
            [InlineData("123456789015")] //za krotki- 12 znakow
            [InlineData("12345678901234567894")] //za dlugi- 20 znakow
            [InlineData("")]
            [InlineData("-")]
            [InlineData("453_228_905_2803")] // 13 znakow, podkreslenia
            [InlineData("4532289052808")] // algorytm luhna bledna ostania cyfra
            [InlineData("4532289052819")] // algorytm luhna bledna przedostania cyfra
            [InlineData("3532289052809")] // algorytm luhna bledna pierwsza cyfra
            [InlineData("4632289052809")] // algorytm luhna bledna druga cyfra
            [InlineData(null)]
            [InlineData("alamakotaalamakotaalaalala")] //typ tekstowy

            public void ValidateCreditCardNumber_ShouldReturnFalse(string cardNumber)
            {
                var credit = new CreditCardService();
                bool result = credit.ValidateCard(cardNumber);
                Assert.False(result);
            }

            [Theory]
            [InlineData("4024-0071-6540-1778", "Visa")] //numery zaczynaja sie od 4
            [InlineData("5530016454538418", "MasterCard")] //51-55 lub 2221-2720
            [InlineData("3497 7965 8312 797", "American Express")] //34 lub 37
            [InlineData("6011 0000 0000 0000", "Discover")] //6011, 65, 644-649
            [InlineData("3589626915830868", "JCB")] //3528-3589
            [InlineData("3050 0000 0000 00", "Diners Club")] //300-305, 36, 38
            [InlineData("5900 0000 0000 0000", "Maestro")] //50, 56-69
            [InlineData("0000 0000 0000 0000", "Unknown")] //nieznany numer
            public void GetCardType_ShouldReturnCorrectCardType(string cardNumber, string expected)
            {
                var credit = new CreditCardService();
                string result = credit.GetCardType(cardNumber);
                Assert.Equal(expected, result);
            }
        }
    }
}