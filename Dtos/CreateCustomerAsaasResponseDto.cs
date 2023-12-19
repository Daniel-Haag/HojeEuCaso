using HojeEuCaso.Interfaces;
using HojeEuCaso.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace HojeEuCaso.Dtos
{
    public class PagamentoListDto
    {
        public string Object { get; set; }
        public bool HasMore { get; set; }
        public int TotalCount { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public List<PagamentoAsaasResponseDto> Data { get; set; }
    }
    public class PagamentoAsaasResponseDto
    {
        public string Object { get; set; }
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Customer { get; set; }
        public string PaymentLink { get; set; }
        public decimal Value { get; set; }
        public decimal NetValue { get; set; }
        public decimal? OriginalValue { get; set; }
        public decimal? InterestValue { get; set; }
        public string Description { get; set; }
        public string BillingType { get; set; }
        public bool CanBePaidAfterDueDate { get; set; }
        public object PixTransaction { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime OriginalDueDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? ClientPaymentDate { get; set; }
        public int? InstallmentNumber { get; set; }
        public string InvoiceUrl { get; set; }
        public string InvoiceNumber { get; set; }
        public string ExternalReference { get; set; }
        public bool Deleted { get; set; }
        public bool Anticipated { get; set; }
        public bool Anticipable { get; set; }
        public DateTime? CreditDate { get; set; }
        public DateTime? EstimatedCreditDate { get; set; }
        public string TransactionReceiptUrl { get; set; }
        public string NossoNumero { get; set; }
        public string BankSlipUrl { get; set; }
        public DateTime? LastInvoiceViewedDate { get; set; }
        public DateTime? LastBankSlipViewedDate { get; set; }

        public DiscountDto Discount { get; set; }
        public FineDto Fine { get; set; }
        public InterestDto Interest { get; set; }

        public bool PostalService { get; set; }
        public object Custody { get; set; }
        public object Refunds { get; set; }
    }

    public class DiscountDto
    {
        public decimal Value { get; set; }
        public DateTime? LimitDate { get; set; }
        public int DueDateLimitDays { get; set; }
        public string Type { get; set; }
    }

    public class FineDto
    {
        public decimal Value { get; set; }
        public string Type { get; set; }
    }

    public class InterestDto
    {
        public decimal Value { get; set; }
        public string Type { get; set; }
    }
}
