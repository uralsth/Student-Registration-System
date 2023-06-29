namespace StudentRegistration.Application.DataTransferObject
{
	public class StudentDocumentCreateDto
	{
		public StudentDocumentCreateDto()
		{
			Documents = new List<AddDocuments>();
		}

		public int StudentId { get; set; }

		public IList<AddDocuments> Documents { get; private set; }

		public AddDocuments Citizenship { get; set; }
		public AddDocuments Passport { get; set; }
		public AddDocuments BankCheque { get; set; }

		public void AddDocuments()
		{
			Documents.Add(Citizenship);
			Documents.Add(Passport);
			Documents.Add(BankCheque);
		}

		//public int Id { get; set; }
		//public string UserName { get; set; }
		//public string Email { get; set; }
		//public string Password { get; set; }
		//public DateTime DateOfBirth { get; set; }
		//public string Address { get; set; }
		//public int Contact { get; set; }
		//public string PhotoPath { get; set; }

		//public List<DocumentUpdateDto> Documents { get; set; }
	}
}