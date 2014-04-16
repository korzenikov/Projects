using System;
using System.Threading.Tasks;

namespace ParallelDataLoadingPrj
{
    public class AsyncLazyBasedLoader
    {
        private AsyncLazy<string> _aData;

        private AsyncLazy<string> _bData;

        private AsyncLazy<string> _cData;

        private AsyncLazy<string> _dData;

        private AsyncLazy<string> _eData;

        public async Task LoadAsync()
        {
            _aData = new AsyncLazy<string>(() => LoadAAsync());
            _bData = new AsyncLazy<string>(() => LoadBAsync());
            _cData = new AsyncLazy<string>(() => LoadCAsync());
            _dData = new AsyncLazy<string>(() => LoadDAsync());
            _eData = new AsyncLazy<string>(() => LoadEAsync());
            await Task.WhenAll(_eData.Value, _dData.Value, _cData.Value, _bData.Value, _aData.Value);
            Console.WriteLine("A: {0}, B: {1}, C: {2}, D: {3}, E: {4}", _aData.Value.Result, _bData.Value.Result, _cData.Value.Result, _dData.Value.Result, _eData.Value.Result);
        }

        private async Task<string> LoadAAsync()
        {
            Console.WriteLine("Load A");
            await Task.Delay(1000);
            return "A";
        }

        private async Task<string> LoadBAsync()
        {
            Console.WriteLine("Load B");
            await Task.Delay(1000);
            var aResult = await _aData;
            return aResult + "B";
        }

        private async Task<string> LoadCAsync()
        {
            Console.WriteLine("Load C");
            await Task.Delay(1000);
            var aResult = await _aData;
            return aResult + "C";
        }

        private async Task<string> LoadDAsync()
        {
            Console.WriteLine("Load D");
            await Task.Delay(1000);
            await Task.WhenAll(_cData.Value, _bData.Value);
            return _bData.Value.Result + _cData.Value.Result + "D";
        }

        private async Task<string> LoadEAsync()
        {
            Console.WriteLine("Load E");
            await Task.Delay(1000);
            var cResult = await _cData;
            return cResult + "E";
        }
    }
}