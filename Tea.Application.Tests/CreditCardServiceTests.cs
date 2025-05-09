using Tea.Application;
using Tea.Domain.Exceptions;
using Tea.Domain.Enums;

namespace Tea.Application.Tests
{
    public class CreditCardServiceTest
    {
        //powinno akceptowac typ tekstowy 13-19 znakow wliczajac myslniki/spacje pomiedzy i zgodny z algorytmem luhna
        //made with luhn algorithm calculator https://simplycalc.com/luhn-calculate.php

        [Theory]
        [InlineData("3497 7965 8312 797")] //prawidlowy, spacje; 15 znakow
        [InlineData("345-470-784-783-010")] //prawidlowy, myslniki; 15 znakow 
        [InlineData("4547078478308668673")] //19 znakow
        [InlineData("4532289052803")] // 13 znakow
        [InlineData("4-5-3---2289052803")] // 18 znakow, wiele myslników 
        [InlineData("453228905 -  -2803")] //13 znakow, myslniki i spacje
        [InlineData("0000000000000")] // algorytm luhna bledna druga cyfra
        [InlineData("1020202010101")] // algorytm luhna poprawny kod bez alternate
        [InlineData("2040404020202")] // algorytm luhna poprawny kod bez alternate
        [InlineData("0001010101010")] // algorytm luhna poprawny kod samo alternate
        public void ValidateCreditCardNumber_ShouldBeCorrect(string cardNumber)
        {
            var credit = new CreditCardService();
            credit.ValidateCard(cardNumber);
            var exception = Record.Exception(() => credit.ValidateCard(cardNumber));
            Assert.Null(exception); // Sprawdzamy, czy nie pojawi sie wyjatek
        }

        [Theory]
        [InlineData("123456789015")] //za krótki- 12 znakow
        [InlineData("")]
        [InlineData("-")]
        [InlineData(null)]
        public void ValidateCreditCardNumber_TooShort(string cardNumber)
        {
            var credit = new CreditCardService();
            Assert.Throws<CardNumberTooShortException>(() => credit.ValidateCard(cardNumber));
        }

        [Theory]
        [InlineData("12345678901234567894")] //za dlugi- 20 znakow
        public void ValidateCreditCardNumber_TooLong(string cardNumber)
        {
            var credit = new CreditCardService();
            Assert.Throws<CardNumberTooLongException>(() => credit.ValidateCard(cardNumber));
        }

        [Theory]
        [InlineData("453_228_905_2803")] // 13 znakow, podkreslenia
        [InlineData("4532289052808")] // algorytm luhna bledna ostania cyfra
        [InlineData("4532289052819")] // algorytm luhna bledna przedostania cyfra
        [InlineData("3532289052809")] // algorytm luhna bledna pierwsza cyfra
        [InlineData("4632289052809")] // algorytm luhna bledna druga cyfra
        [InlineData("alamakotaalalmaka")] //typ tekstowy
        public void ValidateCreditCardNumber_Invalid(string cardNumber)
        {
            var credit = new CreditCardService();
            Assert.Throws<CardNumberInvalidException>(() => credit.ValidateCard(cardNumber));
        }

        [Theory]
        [InlineData("4024-0071-6540-1778", CreditCardProvider.Visa)] //numery zaczynaja sie od 4
        [InlineData("5530016454538418", CreditCardProvider.Mastercard)] //51-55 lub 2221-2720
        [InlineData("3497 7965 8312 797", CreditCardProvider.AmericanExpress)] //34 lub 37
        [InlineData("6011 0000 0000 0000", CreditCardProvider.Discover)] //6011, 65, 644-649
        [InlineData("3589626915830868", CreditCardProvider.JCB)] //3528-3589
        [InlineData("3050 0000 0000 00", CreditCardProvider.DinersClub)] //300-305, 36, 38
        [InlineData("5900 0000 0000 0000", CreditCardProvider.Maestro)] //50, 56-69
        public void GetCardType_ShouldReturnCorrectCardType(string cardNumber, CreditCardProvider? expected)
        {
            var credit = new CreditCardService();
            CreditCardProvider? result = credit.GetCardProvider(cardNumber);
            Assert.Equal(expected, result);
        }
    }
}