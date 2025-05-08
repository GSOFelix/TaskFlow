namespace TaskFlow.Domain.Interfaces.Services
{
    public interface IPassWordService
    {
        /// <summary>
        /// Gera um hash seguro a partir de uma senha em texto puro.
        /// </summary>
        /// <param name="passWord">Senha em texto puro a ser convertida em hash.</param>
        /// <returns>Hash gerado da senha.</returns>
        string Hash(string passWord);

        /// <summary>
        /// Verifica se uma senha em texto puro corresponde a um hash previamente gerado.
        /// </summary>
        /// <param name="passWord">Senha em texto puro a ser verificada.</param>
        /// <param name="hash">Hash com o qual a senha será comparada.</param>
        /// <returns>True se a senha for válida; caso contrário, false.</returns>
        bool Verify(string passWord, string hash);
    }
}
