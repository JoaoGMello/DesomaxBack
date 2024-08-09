namespace DesomaxBack.Common
{
    public enum CodeReturn
    {
        SUCCESS = 200,

        CREATED = 201,

        NO_CONTENT = 204,

        FAILED_REQUEST = -400,

        NOT_AUTHORIZED = -401,

        FORBIDDEN = -403,

        INTERNAL_SERVER_ERROR = -500
    }
}
