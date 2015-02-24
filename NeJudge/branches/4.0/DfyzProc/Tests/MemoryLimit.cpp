const int arrSize = 8 * 1048576;
int nums[arrSize];

int main()
{
	for ( int i = 0; i < arrSize; i++ )
		nums[i] = i;
	int dummy = nums[0];
	for ( int i = 0; i < arrSize; i++ )
		dummy += nums[i];
	return 0;
}
