namespace Assets
{
    public interface IResourceEndpoint
    {
        ResourceUnitScript TryTakeResource();
        bool TryStoreResource(ResourceUnitScript res);
    }
}