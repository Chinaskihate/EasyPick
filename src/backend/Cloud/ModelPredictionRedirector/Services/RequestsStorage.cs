using DraftPrediction.Contract.Models.DataTransferObjects;

namespace ModelPredictionRedirector.Services;

public class RequestsStorage
{
    private List<PredictDraftDto> _data;

    public RequestsStorage()
    {
        _data = new List<PredictDraftDto>();
    }

    public void AddRequest(PredictDraftDto dto)
    {
        _data.Add(dto);
    }

    public IEnumerable<PredictDraftDto> PopRequests()
    {
        var tmp = _data.ToArray();
        _data.Clear();
        return tmp;
    }
}