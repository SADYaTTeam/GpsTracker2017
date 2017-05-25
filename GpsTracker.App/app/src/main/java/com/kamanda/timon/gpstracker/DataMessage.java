
package com.kamanda.timon.gpstracker;

/**
 * Created by timon on 23.05.2017.
 */

public class DataMessage {
    /**
     *
     */
    private double longitude;
    /**
     *
     */
    private double latitude;
    /**
     *
     */
    private String deviceId;
    /**
     *
     */
    private int messageType;

    /**
     *
     */
    public DataMessage() {
//        this.deviceId = Settings.Secure.getString(this.getContentResolver(),
//                Settings.Secure.ANDROID_ID);

    }

    /**
     * @return
     */
    public final double getLongtitude() {
        return longitude; }

    /**
     * @return
     */
    public final double getLatitude() {
        return latitude;
    }

    /**
     * @return deviceId;
     */
    public final String getDeviceId() {
        return deviceId; }

    /**
     * @param deviceId
     */
    public final void getDeviceId(final String deviceId) {
        this.deviceId = deviceId; }

    /**
     * @return
     */
    public final int getMessageType() {
        return messageType;  }

    /**
     * @param longitude
     */
    public void setLongitude(final double longitude) {
        this.longitude = longitude;
    }

    /**
     * @param latitude
     */
    public void setLatitude(final double latitude) {
        this.latitude = latitude;
    }

    /**
     * @param deviceId
     */
    public final void setDeviceId(final String deviceId) {
        this.deviceId = deviceId; }

    /**
     * @param messageType
     */
    public final void setMessageType(final int messageType) {
        this.messageType = messageType; }


}
