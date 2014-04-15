using System;
using System.Threading.Tasks;

namespace ParallelDataLoadingPrj
{
    public class TaskBasedExecutor
    {
        private string _aData;
        private string _bData;
        private string _cData;
        private string _dData;
        private string _eData;

        private async Task LoadAAsync()
        {
            Console.WriteLine("Load A");
            await Task.Delay(1000);
            _aData = "A";
            
        }

        private async Task LoadBAsync()
        {
            Console.WriteLine("Load B");
            await Task.Delay(1000);
            _bData =_aData + "B";
        }

        private async Task LoadCAsync()
        {
            Console.WriteLine("Load C");
            await Task.Delay(1000);
            _cData = _aData + "C";
        }

        private async Task LoadDAsync()
        {
            Console.WriteLine("Load D");
            await Task.Delay(1000);
            _dData = _bData + _cData + "D";
        }

        private async Task LoadEAsync()
        {
            Console.WriteLine("Load E");
            await Task.Delay(1000);
            _eData = _cData + "E";
        }
        
        public async Task IntializeAsync()
        {
            var taskA = LoadAAsync();
            var taskC = taskA.Then(() => LoadCAsync());
            var taskE = taskC.Then(() => LoadEAsync());
            var taskB = taskA.Then(() => LoadBAsync());
            var taskD = Task.WhenAll(taskC, taskB).Then(() => LoadDAsync());
            
            await Task.WhenAll(taskE, taskD, taskA, taskB, taskC);
            Console.WriteLine("A: {0}, B: {1}, C: {2}, D: {3}, E: {4}", _aData, _bData, _cData, _dData, _eData);
        }


       
    }
}