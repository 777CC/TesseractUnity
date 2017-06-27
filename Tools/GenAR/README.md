genTexData
genTexData performs training of NFT datasets from a supplied JPEG-format source image.

Usage:

    ./genTexData <filename>
        -level=n
             (n is an integer in range 0 (few) to 4 (many). Default 2.'
        -sd_thresh=<sd_thresh>
        -max_thresh=<max_thresh>
        -min_thresh=<min_thresh>
        -leveli=n
             (n is an integer in range 0 (few) to 3 (many). Default 1.'
        -feature_density=<feature_density>
        -dpi=<dpi>
        -max_dpi=<max_dpi>
        -min_dpi=<min_dpi>
        -background
             Run in background, i.e. as daemon detached from controlling terminal. (Mac OS X and Linux only.)
        -log=<path>
        -loglevel=x
             x is one of: DEBUG, INFO, WARN, ERROR. Default is INFO.
        -exitcode=<path>
        --help -h -?  Display this help