namespace Assets.Sources.SaveLoadData
{
    public interface ISaveLoadService
    {
        void Save<TData>(TData data);

        TData TryLoad<TData>()
            where TData : class;
    }
}