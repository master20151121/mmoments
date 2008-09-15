This hello world reads a song (wav) and calculates its fingerprint using libfooid.

fails atm, not sure how im supposed to import fp_calculate(), im getting access violation error. might not be a problem with the import.

LIBRARY	"FooID"
EXPORTS
	fp_init
	fp_free
	fp_feed_short
	fp_feed_float
	fp_getsize
	fp_getversion
	fp_calculate
must ingore t_fooid struct in such lines, because its not exported it must be controlled by library.
FOOIDAPI t_fooid * fp_init(int samplerate, int channels);
