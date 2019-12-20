<template>
    <div>
        <Modal
                :title="L('EditBanner')"
                :value="value"
                @on-ok="save"
                @on-visible-change="visibleChange"
        >
            <Form ref="bannerForm"  label-position="top" :rules="bannerRule" :model="banner">
                <Tabs value="detail">
                    <TabPane :label="L('UserDetails')" name="detail">
                        <FormItem :label="L('Image')" prop="bannerImg">
                            <div class="demo-upload-list" v-for="item in uploadList">
                                <template v-if="item.status === 'finished'">
                                    <img v-if="item.name" :src="`${ url }Upload/Banner/${ item.name }`">
                                    <div class="demo-upload-list-cover">
                                        <Icon type="ios-eye-outline" @click.native="handleView()"></Icon>
                                        <Icon type="ios-trash-outline" @click.native="handleRemove(item)"></Icon>
                                    </div>
                                </template>
                                <template v-else>
                                    <Progress v-if="item.showProgress" :percent="item.percentage" hide-info></Progress>
                                </template>
                            </div>
                            <Input style="display: none" v-model="banner.bannerImg"></Input>
                            <Upload ref="upload"
                                    :action="`${ url }api/services/app/Banner/OnPostUploadBannerImg`"
                                    :headers="header"
                                    :format="['jpg','jpeg','png']"
                                    :max-size="2048"
                                    :default-file-list="defaultList"
                                    :show-upload-list="false"
                                    :on-success="handleSuccess"
                                    :on-format-error="handleFormatError"
                                    :on-exceeded-size="handleMaxSize"
                                    :on-error="handleError"
                                    :before-upload="handleBeforeUpload"
                                    :on-remove="handleRemove">
                                <Button icon="ios-cloud-upload-outline">{{ L('UploadFiles') }}</Button>
                            </Upload>
                        </FormItem>
                        <FormItem :label="L('Theme')" prop="theme">
                            <Input v-model="banner.theme" :maxlength="200"></Input>
                        </FormItem>
                        <FormItem :label="L('Link')" prop="bannerLink">
                            <Input v-model="banner.bannerLink" :maxlength="500"></Input>
                        </FormItem>
                        <FormItem :label="L('Type')" prop="linkType">
                            <RadioGroup v-model="banner.linkType">
                                <Radio :label="0" true-value="0">
                                    <span>{{ L('WePlatform') }}</span>
                                </Radio>
                                <Radio :label="1" true-value="1">
                                    <span>{{ L('ThirdPlatform') }}</span>
                                </Radio>
                            </RadioGroup>
                        </FormItem>
                        <FormItem :label="L('Sequence')" prop="bannerOrder">
                            <InputNumber v-model="banner.bannerOrder"></InputNumber>
                        </FormItem>
                        <FormItem :label="L('Type')" prop="bannerType">
                            <Select :placeholder="L('Select')" v-model="banner.type">
                                <Option v-for="(item, index) in bannerType" :key="index" :value="item.value">{{L(item.name)}}</Option>
                            </Select>
                        </FormItem>
                        <FormItem :label="L('ShowBeginDate')" prop="showBeginDate">
                            <DatePicker  type="date" v-model="banner.showBeginDate" @on-change="showBeginDate" format="yyyy-MM-dd" style="width:100%" placement="top-end" :placeholder="L('SelectDate')"></DatePicker>
                        </FormItem>
                        <FormItem :label="L('ShowEndDate')" prop="showEndDate">
                            <DatePicker  type="date" v-model="banner.showEndDate" @on-change="showEndDate" format="yyyy-MM-dd" style="width:100%" placement="bottom-end" :placeholder="L('SelectDate')"></DatePicker>
                        </FormItem>
                    </TabPane>
                </Tabs>
            </Form>
            <div slot="footer">
                <Button @click="cancel">{{L('Cancel')}}</Button>
                <Button @click="save" type="primary">{{L('OK')}}</Button>
            </div>
        </Modal>

        <Modal :title="L('ViewImage')" v-model="visible">
            <img :src="`${ url }Upload/Banner/${ banner.bannerImg }`" v-if="visible" style="width: 100%">
        </Modal>
    </div>
</template>
<script lang="ts">
    import { Component, Vue,Inject, Prop,Watch } from 'vue-property-decorator';
    import Util from '../../../lib/util'
    import AbpBase from '../../../lib/abpbase'
    import Banner from '../../../store/entities/banner'
    import url from '../../../lib/url'

    @Component
    export default class EditBanner extends AbpBase{
        @Prop({type:Boolean,default:false}) value:boolean;
        banner:Banner=new Banner();
        uploadList:Array<any> = []
        defaultList:Array<any> = []
        visible:Boolean = false
        header:Object = { Authorization: "Bearer "+ window.abp.auth.getToken() }

        get bannerType(){
            return this.$store.state.banner.bannerType;
        }
        get editBanner(){
            return this.$store.state.banner.editBanner;
        }
        get url(){
            return url;
        }

        handleSuccess (res, file) {
            this.banner.bannerImg = res.result.fileName;
            file.name = res.result.fileName;
        }
        handleFormatError (file) {
            this.$Notice.warning({
                title: this.L('UploadFormatIncorrect'),
                desc: this.L('UploadFormatIncorrectTip', undefined, file.name)
            });
        }
        handleMaxSize (file) {
            this.$Notice.warning({
                title: this.L('UploadExceedingSize'),
                desc: this.L('UploadExceedingSizeTip', undefined, file.name, '1M')
            });
        }
        handleError (error) {
            this.$Notice.warning({
                title: this.L('UploadFail')
            });
        }
        handleBeforeUpload () {
            if (this.$refs.upload['fileList'].length > 0) {
                this.$refs.upload['fileList'].splice(0, 1);
            }
            return true
        }
        handleView () {
            this.visible = true;
        }
        handleRemove (file) {
            const fileList = this.$refs.upload['fileList'];
            this.$refs.upload['fileList'].splice(fileList.indexOf(file), 1);
            this.banner.bannerImg = "";
        }

        showBeginDate(date) {
            this.banner.showBeginDate = date
        }
        showEndDate(date) {
            this.banner.showEndDate = date
        }
        save(){
            (this.$refs.bannerForm as any).validate(async (valid:boolean)=>{
                if(valid){
                    await this.$store.dispatch({
                        type:'banner/update',
                        data:this.banner
                    });
                    (this.$refs.bannerForm as any).resetFields();
                    this.$emit('save-success');
                    this.$emit('input',false);
                }
            })
        }
        cancel(){
            (this.$refs.bannerForm as any).resetFields();
            this.$emit('input',false);
        }
        visibleChange(value:boolean){
            if(!value){
                this.$emit('input',value);
            }else{
                this.banner=Util.extend(true,{},this.editBanner);
                this.defaultList = [{
                    name: this.banner.bannerImg,
                    url: `${ url }Upload/Banner/${ this.banner.bannerImg }`
                }]

                this.$nextTick(()=>{
                    this.uploadList = this.$refs.upload['fileList'];
                })
            }
        }
        bannerRule={
            theme:[{required: true,message:this.L('FieldIsRequired',undefined,this.L('Theme')),trigger: 'blur'}],
            bannerImg:[{required: true,message:this.L('FieldIsRequired',undefined,this.L('Banner')),trigger: 'blur'}]
        }
    }
</script>

<style>
    .demo-upload-list{
        display: inline-block;
        width: 60px;
        height: 60px;
        text-align: center;
        line-height: 60px;
        border: 1px solid transparent;
        border-radius: 4px;
        overflow: hidden;
        background: #fff;
        position: relative;
        box-shadow: 0 1px 1px rgba(0,0,0,.2);
        margin-right: 4px;
    }
    .demo-upload-list img{
        width: 100%;
        height: 100%;
    }
    .demo-upload-list-cover{
        display: none;
        position: absolute;
        top: 0;
        bottom: 0;
        left: 0;
        right: 0;
        background: rgba(0,0,0,.6);
    }
    .demo-upload-list:hover .demo-upload-list-cover{
        display: block;
    }
    .demo-upload-list-cover i{
        color: #fff;
        font-size: 20px;
        cursor: pointer;
        margin: 0 2px;
    }
</style>