using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidacijaAbc
{
	public class Osoba : INotifyPropertyChanged, IDataErrorInfo
	{
		private string _ime;
		public string Ime 
		{ 
			get => _ime; 
			set
			{
				_ime = value;
				Promena("Ime");
			}
		}
		private string _prezime;
		public string Prezime 
		{ 
			get => _prezime; 
			set
			{
				_prezime = value;
				Promena("Prezime");
			}
		}
		private string _email { get; set; }
		public string Email 
		{ 
			get => _email; 
			set
			{
				_email = value;
				Promena("Email");
			}
		}
		private int _godiste;
		public int Godiste 
		{ 
			get => _godiste; 
			set
			{
				_godiste = value;
				Promena("Godiste");
			}
		}

		private OsobaValidator _validator;
		public void Validiraj()
			=> _validator = new();
		public string this[string imePropertija]
		{
			get
			{
				if (_validator is null)
					return string.Empty;
				var greske = _validator.Validate(this);
				var greska = greske.Errors.Where(greska => greska.PropertyName == imePropertija)
					.FirstOrDefault();
				if (greska is not null)
					return greska.ErrorMessage;
				return string.Empty;
			}
		}

		public void Promena(string imePropertija)
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(imePropertija));

		public event PropertyChangedEventHandler PropertyChanged;
		public string Error => throw new NotImplementedException();
	}

	public class OsobaValidator : AbstractValidator<Osoba>
	{
		public OsobaValidator()
		{
			RuleFor(o => o.Ime).NotEmpty().WithMessage("Nista prazno")
				.MinimumLength(3).WithMessage("Premalo")
				.MaximumLength(50).WithMessage("Previse");
			RuleFor(o => o.Prezime).NotEmpty().WithMessage("Nista prazno")
				.MinimumLength(3).WithMessage("Premalo")
				.MaximumLength(50).WithMessage("Previse");
			RuleFor(o => o.Email).NotEmpty().WithMessage("Nista prazno")
				.EmailAddress().WithMessage("Nije mejl");
			RuleFor(o => o.Godiste).InclusiveBetween(0, 10).WithMessage("Jook");
		}
	}
}
