using ChromaSDK;
using System;

namespace CSharp_ChromaGameLoopSample
{
    public class SampleApp
    {
        private int _mResult = 0;

        public int GetInitResult()
        {
            return _mResult;
        }

        public void Start()
        {
            _mResult = ChromaAnimationAPI.Init();
            switch (_mResult)
            {
                case RazerErrors.RZRESULT_DLL_NOT_FOUND:
                    Console.Error.WriteLine("Chroma DLL is not found! {0}", RazerErrors.GetResultString(_mResult));
                    break;
                case RazerErrors.RZRESULT_DLL_INVALID_SIGNATURE:
                    Console.Error.WriteLine("Chroma DLL has an invalid signature! {0}", RazerErrors.GetResultString(_mResult));
                    break;
                case RazerErrors.RZRESULT_SUCCESS:
                    break;
                default:
                    Console.Error.WriteLine("Failed to initialize Chroma! {0}", RazerErrors.GetResultString(_mResult));
                    break;
            }
        }
        public void OnApplicationQuit()
        {
            ChromaAnimationAPI.Uninit();
        }
    }
}
