# Capture point realization

This document serves to give more insight into the process of going from a first concept of the flags to the final product. This is done on a per sprint basis. Within each section below will be information given regarding what was achieved in a singular sprint and how these contributions helped in the iterative realisation of the hardware prototype.

## Sprint 0
During this preparation period the group shortly discussed how the challenge should be translated into the first concept. We wrote a challenge debrief from which this important relevant analysis information came. 

```
Focus of new hardware > points to improve upon:

- Visibility
- Scoring mechanics
- Interaction difficulty
```

With the initial budget of about 600 euros we set out to design 3 capture points for 200 each.

## Sprint 1
In sprint 1 I started with researching which possible microcontrollers we could use and what kind of peripherals we should attach to the system. The current flagpoles were about 5 meters high and the ceiling in the new arena would only be 5-6 meters above so if we wanted to design new flag poles they would have to be about 4-4.5 meters high.

```
Main board: ESP32 
Height: 4/4.5 meters 
Main lighting: Diffused WS2812B NEOPIXEL 
Top light: Some kind of siren light or LED matrix 
Encryption: RFID (Tags, cards, wristbands), Keypad encryption
Diagnostics: Small OLED screen 
```
With these technical requirements and hardware specs we designed a basic visualization of what the flagpoles could possibly look like. We made 2 visualizations based upon what would could possibly attract the most attention in our mind.

<a href="https://ibb.co/68mC4zn"><img src="https://i.ibb.co/5k6H5mM/Teams-Bv-BWpa-FEWd.png" alt="Teams-Bv-BWpa-FEWd" border="0"></a>

The first budget calculation with designing the flags like this was established relatively fast and can be seen below.

<table border="0">
 <colgroup><col>
 <col >
 <col >
 <col >
 <col >
 </colgroup><tbody>
 <tr class="xl658825" height="20" style="height:15.0pt">
  <td >Component</td>
  <td >Link</td>
  <td >Quantity</td>
  <td >Unit price</td>
  <td>Total price</td>
 </tr>
 <tr height="20" style="height:15.0pt">
  <td >WS2812B LED STRIP</td>
  <td ><a href="https://nl.aliexpress.com/item/2036819167.html?spm=a2g0o.productlist.0.0.3b0514d4i5MUgF&amp;algo_pvid=584178a8-0ca3-4991-81de-9179eecb98bf&amp;aem_p4p_detail=2022030704501445642828267840000792194&amp;algo_exp_id=584178a8-0ca3-4991-81de-9179eecb98bf-1&amp;pdp_ext_f=%7B%22sku_id%22%3A%2267389781291%22%7D&amp;pdp_pi=-1%3B11.48%3B-1%3B-1%40salePrice%3BEUR%3Bsearch-mainSearch">https://nl.aliexpress.com/item/2036819167.html?spm=a2g0o.productlist</a></td>
  <td >3</td>
  <td >€20,30 </td>
  <td >€60,90 </td>
 </tr>
 <tr height="20" style="height:15.0pt">
  <td >WS2812B LED Matrix</td>
  <td ><a href="https://nl.aliexpress.com/item/33025679652.html?_randl_currency=EUR&amp;_randl_shipto=NL&amp;src=google&amp;src=google&amp;albch=shopping&amp;acnt=494-037-6276&amp;slnk=&amp;plac=&amp;mtctp=&amp;albbt=Google_7_shopping&amp;albagn=888888&amp;isSmbAutoCall=false&amp;needSmbHouyi=false&amp;albcp=9317229739&amp;albag=97939647727&amp;trgt=536572975094&amp;crea=nl33025679652&amp;netw=u&amp;device=c&amp;albpg=536572975094&amp;albpd=nl33025679652&amp;gclid=Cj0KCQiA95aRBhCsARIsAC2xvfw_OJSxyJTA-SVojZZUtsOPG63JbEgltVEouWAGG42J3Pf6yibcKo4aAhREEALw_wcB&amp;gclsrc=aw.ds&amp;aff_fcid=5009e9b824bc48868db17be6f1a4bb00-1646656828963-01391-UneMJZVf&amp;aff_fsk=UneMJZVf&amp;aff_platform=aaf&amp;sk=UneMJZVf&amp;aff_trace_key=5009e9b824bc48868db17be6f1a4bb00-1646656828963-01391-UneMJZVf&amp;terminal_id=93dd53c70cdb442dba05d09f60d9da7d&amp;afSmartRedirect=y">https://nl.aliexpress.com/item/33025679652.html?_randl_currency=EUR</a></td>
  <td >6</td>
  <td>€16,75</td>
  <td >€100,50 </td>
 </tr>
 <tr height="20" style="height:15.0pt">
  <td >Acryl Tube</td>
  <td ><a href="https://kunststofshop.nl/acrylaat-plexiglas/acrylaat-buizen/melkwit-opaal/acrylaat-buis-opaal-2000x40x3mm-2000x40x3mm/a-6130-20000036">https://kunststofshop.nl/acrylaat-plexiglas/acrylaat-buizen/melkwit-op<span style="display:none">aal/acrylaat-buis-opaal-2000x40x3mm-2000x40x3mm/a-6130-20000036</span></a></td>
  <td >6</td>
  <td >€34,50 </td>
  <td >€207,00 </td>
 </tr>
 <tr height="20" style="height:15.0pt">
  <td >LILYGO TTGO T-DISPLAY
  v1.1 ESP32</td>
  <td ><a href="https://www.tinytronics.nl/shop/en/development-boards/microcontroller-boards/with-wi-fi/lilygo-ttgo-t-display-v1.1-esp32-with-1.14-inch-tft-display">https://www.tinytronics.nl/shop/en/development-boards/microcontroller-boards/with-wi-fi/lilygo-ttgo-t-display-v1.1-esp32-with-1.14-inch-tft-display</a></td>
  <td >3</td>
  <td >€13,50 </td>
  <td >€40,50 </td>
 </tr>
 <tr height="20" style="height:15.0pt">
  <td >MFRC522 RFID Kit</td>
  <td ><a href="https://www.tinytronics.nl/shop/en/communication-and-signals/wireless/rfid/rfid-kit-mfrc522-s50-mifare-with-card-and-key-tag">https://www.tinytronics.nl/shop/en/communication-and-signals/wireless/rfid/rfid-kit-mfrc522-s50-mifare-with-card-and-key-tag</a></td>
  <td >3</td>
  <td >€5,50 </td>
  <td >€16,50 </td>
 </tr>
 <tr height="20" style="height:15.0pt">
  <td >Keypad</td>
  <td><a href="https://www.amazon.nl/dp/B07ZT2RRT1/?coliid=I3LGUMVUDYYJ19&amp;colid=9EX3SF8SLQV8&amp;psc=1&amp;ref_=lv_ov_lig_dp_it_im">https://www.amazon.nl/dp/B07ZT2RRT1/?coliid=I3LGUMVUDYYJ19</a></td>
  <td >3</td>
  <td >€5,00 </td>
  <td >€15,00 </td>
 </tr>
 <tr height="20" style="height:15.0pt">
  <td >5v 40A power supply</td>
  <td ><a href="https://www.amazon.nl/Schakelende-Universele-Stroomvoorziening-Stroomadapter-Transformator/dp/B07Y38SMQ3/ref=sr_1_4?__mk_nl_NL=%C3%85M%C3%85%C5%BD%C3%95%C3%91&amp;crid=2MI848YVJX7MO&amp;keywords=5v+300+watt+voeding&amp;qid=1646658837&amp;sprefix=5v+300+watt+voeding%2Caps%2C55&amp;sr=8-4">https://www.amazon.nl/Schakelende-Universele-Stroomvoorziening-St<span style="display:none">roomadapter-Transformator/dp/B07Y38SMQ3/ref=sr_1_4?__mk_nl_NL=%C3%85M%C3%85%C5%BD%C3%95%C3%91&amp;crid=2MI848YVJX7MO&amp;keywords=5v+300+watt+voeding&amp;qid=1646658837&amp;sprefix=5v+300+watt+voeding%2Caps%2C55&amp;sr=8-4</span></a></td>
  <td >3</td>
  <td >€27,00</td>
  <td >€81,00</td>
 </tr>
</tbody></table>


## Sprint 2


## Sprint 3