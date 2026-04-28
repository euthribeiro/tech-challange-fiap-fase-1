namespace wrench.auto.repair.core.Errors
{
    public enum TipoErroEnum
    {
        ENTIDADE_NAO_PROCESSAVEL,

        // Retorna Bad Request
        VALIDACAO,

        // Retornar NotFound
        NAO_ENCONTRADO,

        // Retorna Conflict
        CONFLITO,

        // Retornar Inesperado
        INESPERADO,

        // Não autorizado
        NAO_AUTORIZADO,

        // Retornar Forbidden
        PROIBIDO
    }
}
