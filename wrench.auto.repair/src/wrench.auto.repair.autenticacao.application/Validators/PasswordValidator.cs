using FluentValidation;

namespace wrench.auto.repair.autenticacao.application.Validators
{
    public class PasswordValidator : AbstractValidator<string>
    {
        private static readonly int MinimumPasswordLength = 12;
        private static readonly int MediumPasswordLengthThreshold = 13;
        private static readonly int MinimumCharacterTypesForShortPassword = 3;
        private static readonly int MinimumCharacterTypesForLongPassword = 2;

        private static readonly int MaxConsecutiveIdenticalCharacters = 4;
        private static readonly int MaxConsecutiveKeyboardCharacters = 4;
        private static readonly int MaxAlphabeticalSequenceLength = 4;

        private static readonly int SensitiveDataSequenceLength = 4;
        private static readonly int PhoneNumberSequenceLength = 4;

        private readonly string _phoneNumber;
        private readonly string? _idNumber;
        private readonly DateTime? _dob;

        public PasswordValidator(string phoneNumber, string? idNumber, DateTime? dob)
        {
            _phoneNumber = phoneNumber ?? string.Empty;
            _idNumber = idNumber;
            _dob = dob;

            // Senha não pode ser nulo o vazio
            RuleFor(password => password)
                .NotEmpty()
                .WithMessage("Senha não pode ser nulo");

            // Tamanha da Senha e Requisitos
            RuleFor(password => password)
                .Must(ValidateLength)
                .WithMessage(
                    $"Senha deve ter pelo menos {MinimumPasswordLength} caracteres e atender a todos os requisitos de senha.");

            // Pelo menos um caractere de letra maíuscula
            RuleFor(password => password)
                .Must(password => password.Any(char.IsUpper))
                .WithMessage("A senha deve ter pelo menos uma letra maiúscula");

            // Sem excesso de caracteres idênticos consecutivos
            RuleFor(password => password)
                .Must(password =>
                    !ContainsIdenticalCharacters(password, MaxConsecutiveIdenticalCharacters))
                .WithMessage(
                    $"A senha não pode conter {MaxConsecutiveIdenticalCharacters} ou mais caracteres idênticos consecutivos");

            // Sem excesso de layout de teclado consecutivos
            RuleFor(password => password)
                .Must(password =>
                    !ContainsConsecutiveKeyboardLayout(password, MaxConsecutiveKeyboardCharacters))
                .WithMessage(
                    $"A senha não pode conter {MaxConsecutiveKeyboardCharacters} um ou mais caracteres de layout de teclado consecutivos");

            // Sem sequências de alfabeto
            RuleFor(password => password)
                .Must(password =>
                    !ContainsAlphabeticalSequence(password, MaxAlphabeticalSequenceLength))
                .WithMessage(
                    $"A senha não pode conter {MaxAlphabeticalSequenceLength} ou mais caracteres de sequência de alfabeto");

            // Sem dados sensíveis (identificador ou data de nascimento)
            RuleFor(password => password)
                .Must(password => !ContainsSensitiveDataDigits(password, _idNumber, _dob,
                    SensitiveDataSequenceLength))
                .WithMessage(
                    $"A senha não pode conter parte de dados sensíveis de identificadores ou data de nascimento");

            // Sem sequências de telefone
            RuleFor(password => password)
                .Must(password =>
                    !ContainsPhoneNumberSequence(password, _phoneNumber, PhoneNumberSequenceLength))
                .WithMessage(
                    $"A senha não pode conter partes do número de telefone cadastro");
        }

        private bool ValidateLength(string password)
        {
            if (password.Length < MinimumPasswordLength)
                return false;

            if (password.Length >= MinimumPasswordLength &&
                password.Length < MediumPasswordLengthThreshold &&
                !MeetsThreeTypeCriteria(password))
                return false;

            if (password.Length >= MediumPasswordLengthThreshold &&
                !MeetsTwoTypeCriteria(password))
                return false;

            return true;
        }

        private static bool MeetsThreeTypeCriteria(string password)
        {
            int typesCount = 0;
            if (password.Any(char.IsUpper)) typesCount++;
            if (password.Any(char.IsLower)) typesCount++;
            if (password.Any(char.IsDigit)) typesCount++;
            if (password.Any(ch => !char.IsLetterOrDigit(ch))) typesCount++;

            return typesCount >= MinimumCharacterTypesForShortPassword;
        }

        private static bool MeetsTwoTypeCriteria(string password)
        {
            int typesCount = 0;
            if (password.Any(char.IsUpper)) typesCount++;
            if (password.Any(char.IsLower)) typesCount++;
            if (password.Any(char.IsDigit)) typesCount++;
            if (password.Any(ch => !char.IsLetterOrDigit(ch))) typesCount++;

            return typesCount >= MinimumCharacterTypesForLongPassword;
        }

        private static bool ContainsIdenticalCharacters(string password, int length)
        {
            for (int i = 0; i <= password.Length - length; i++)
            {
                if (password.Skip(i).Take(length).All(c => c == password[i]))
                    return true;
            }

            return false;
        }

        private static bool ContainsConsecutiveKeyboardLayout(string password, int length)
        {
            string[] keyboardRows = { "1234567890", "qwertyuiop", "asdfghjkl", "zxcvbnm" };
            foreach (var row in keyboardRows)
            {
                for (int i = 0; i <= row.Length - length; i++)
                {
                    string sequence = row.Substring(i, length);
                    if (password.Contains(sequence, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }

            return false;
        }

        private static bool ContainsAlphabeticalSequence(string password, int length)
        {
            for (int i = 0; i <= password.Length - length; i++)
            {
                if (password.Skip(i).Take(length).Select(c => char.ToLower(c))
                    .SequenceEqual(Enumerable.Range('a', length).Select(x => (char)x)))
                    return true;
            }

            return false;
        }

        private static bool ContainsPhoneNumberSequence(string password, string phoneNumber, int length)
        {
            if (string.IsNullOrEmpty(phoneNumber)) return false;

            for (int i = 0; i <= phoneNumber.Length - length; i++)
            {
                string sequence = phoneNumber.Substring(i, length);
                if (password.Contains(sequence, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        private static bool ContainsSensitiveDataDigits(string password, string? idNumber, DateTime? dob, int length)
        {
            var sensitiveData = new List<string>();
            if (!string.IsNullOrEmpty(idNumber))
            {
                sensitiveData.Add(idNumber); // Decrypt if necessary
            }

            if (dob.HasValue)
            {
                sensitiveData.Add(dob.Value.ToString("yyyyMMdd"));
            }

            foreach (var data in sensitiveData)
            {
                for (int i = 0; i <= data.Length - length; i++)
                {
                    string sequence = data.Substring(i, length);
                    if (password.Contains(sequence, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }

            return false;
        }
    }
}
