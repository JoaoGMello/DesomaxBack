namespace DesomaxBack.Common
{
    public class JsonReturn
    {
        public CodeReturn Code { get; set; }

        public bool Success { get; set; }

        public string? Message { get; set; }

        public object? Data { get; set; }

        public void SetFailedRequest(string message = "Falha na requisição")
        {
            Code = CodeReturn.FAILED_REQUEST;
            Message = message;
        }

        public void SetSuccess(object data, string message = "Sucesso")
        {
            Data = data;
            Code = CodeReturn.SUCCESS;
            Message = message;
            Success = true;
        }

        public void SetSuccess(string message = "Sucesso")
        {
            Code = CodeReturn.SUCCESS;
            Message = message;
            Success = true;
        }

        public void SetInternalError(string message = "Erro interno")
        {
            Code = CodeReturn.INTERNAL_SERVER_ERROR;
            Message = message;
            Success = false;
        }

        public void SetNoContent(string message = "Sem conteúdo")
        {
            Code = CodeReturn.NO_CONTENT;
            Message = message;
        }

        public void SetnotAuthorized(string message = "Não Autorizado")
        {
            Code = CodeReturn.NOT_AUTHORIZED;
            Message = message;
        }

        public void SetForbidden(string message = "Proibido")
        {
            Code = CodeReturn.FORBIDDEN;
            Message = message;
        }
    }
}
